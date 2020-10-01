// .NET Framework
using System.Collections.Generic;


// Unity Framework
using UnityEngine;
using UnityEngine.Audio;

namespace FrameLord
{
    public class SoundManager : MonoBehaviorSingleton<SoundManager>
    {
	    // Total number of channels
        public int channels = 16;

        // Min volume in dB
        public float minVolumeDb = -80f;
        
        // Max volume in dB
        public float maxVolumeDb = 20f;

        // Audio mixer reference
        public AudioMixer audioMixer; 
        
        // AudioSources array for sound f/x
        private AudioSource[] _fxASList;
        
        // SoundList array
        private Dictionary<string, SoundManagerClip> _soundManagerClips;
        
        // List to relate the audioSoruce to the soundManagerClips that is in use
        private SoundManagerClip[] _listOfClipsInUse;

        // List of faders
        private List<FadeAudioSource> _listFaders;

        // List of channels to resume (that were playing at pause)
        private List<int> _channelsToResume;

        /// <summary>
        /// Unity Awake Method
        /// </summary>
        new void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
           
            _listFaders = new List<FadeAudioSource>();
            _listOfClipsInUse = new SoundManagerClip[channels];
            
            CreateAudioSources();
            GetAllSoundManagerClips();
        }
        
        /// <summary>
        /// Unity Update Method
        /// </summary>
        void Update()
        {
	        UpdateAllFaders();
        }

        /// <summary>
        /// Update all the faders
        /// </summary>
        private void UpdateAllFaders()
        {
	        foreach (FadeAudioSource fas in _listFaders)
	        {
		        fas.accumTime = Time.realtimeSinceStartup - fas.initialTime;

		        // Increase the volume
		        float totalDelta = fas.targetVolume - fas.initialVolume;
		        float deltaVolume = totalDelta * (fas.accumTime / fas.fadeInSecs);

		        fas.audioSrc.volume = fas.initialVolume + deltaVolume;
		        
		        if (fas.accumTime >= fas.fadeInSecs)
		        {
			        fas.audioSrc.volume = fas.targetVolume;

			        // Remove the fading
			        _listFaders.Remove(fas);

			        // Call the event
			        fas.fnCb?.Invoke();
			        
			        // Return on every remove
			        return;
		        }
	        }
        }
        
        /// <summary>
        /// Create all the component audiosources
        /// </summary>
        private void CreateAudioSources()
        {
            // Add many audiosources for multiple sounds
            _fxASList = new AudioSource[channels];
            for (var i = 0; i < channels; i++)
            {
                _fxASList[i] = gameObject.AddComponent<AudioSource>();
                _fxASList[i].playOnAwake = false;
            }
        }

        /// <summary>
        /// Get all the SoundManagerClips
        /// </summary>
        private void GetAllSoundManagerClips()
        {
	        _soundManagerClips = new Dictionary<string, SoundManagerClip>();
	        
            var clips = GetComponentsInChildren<SoundManagerClip>();
            for (int i = 0; i < clips.Length; i++)
            {
                _soundManagerClips.Add(clips[i].name, clips[i]);
            }
        }

        /// <summary>
        /// Add sound manager clips
        /// </summary>
        public void AddSoundManagerClips(List<SoundManagerClip> listOfSoundManagerClips)
        {
	        for (int i = 0; i < listOfSoundManagerClips.Count; i++)
	        {
		        if (!_soundManagerClips.ContainsKey(listOfSoundManagerClips[i].name))
		        {
			        _soundManagerClips.Add(listOfSoundManagerClips[i].name, listOfSoundManagerClips[i]);
		        }
		        else
		        {
			        Debug.LogError("[SoundManager] Trying to add a sound manager clip that has been added before.");
		        }
	        }
        }
        
        /// <summary>
        /// Remove sound manager clips
        /// </summary>
        public void RemoveSoundManagerClips(List<SoundManagerClip> listOfSoundManagerClips)
        {
	        for (int i = 0; i < listOfSoundManagerClips.Count; i++)
	        {
		        if (_soundManagerClips.ContainsKey(listOfSoundManagerClips[i].name))
		        {
			        _soundManagerClips.Remove(listOfSoundManagerClips[i].name);
		        }
	        }
        }

        /// <summary>
        /// Play the specified sound. Returns -1 if there isn't any audiosource available to play the sound.
        /// </summary>
        public int PlaySound(string sndId)
        {
	        if (string.IsNullOrEmpty(sndId)) return -1;
            return PlaySound(Vector3.zero, sndId);
        }

        /// <summary>
        /// Play the specified sound. Returns -1 if there isn't any audiosource available to play the sound.
        /// </summary>
        public int PlaySound(Vector3 pos, string sndId)
        {
            Debug.Assert(_soundManagerClips.ContainsKey(sndId));
            var smc = _soundManagerClips[sndId];

            var chnId = GetChannelIdx(smc);
            
            if (chnId != -1)
            {
	            PlayThisSoundOnSource(chnId, smc, pos);
	            return chnId;
            }
            else
            {
	            Debug.LogWarningFormat("[SoundManager] All audiosource are busy. Cannot play sound {0}", smc.name);
            }

            return -1;
        }

        /// <summary>
        /// Stop the specified sound channel
        /// </summary>
        public void StopSound(int chnId)
        {
	        _fxASList[chnId].Stop();
        }

        /// <summary>
        /// Pause the specified sound
        /// </summary>
        public void PauseSound(int chnId)
        {
	        _fxASList[chnId].Pause();
        }
        
        /// <summary>
        /// Pause the specified sound
        /// </summary>
        public void ResumeSound(int chnId)
        {
	        _fxASList[chnId].Play();
        }

        /// <summary>
        /// Returns if the specified channel id is playing
        /// </summary>
        public bool IsPlaying(int chnId)
        {
	        return _fxASList[chnId].isPlaying;
        }

        /// <summary>
        /// Stop all sounds in all channels
        /// </summary>
        public void StopAll()
        {
	        for (int i = 0; i < _fxASList.Length; i++)
	        {
		        _fxASList[i].Stop();
	        }
        }

        /// <summary>
        /// Pause all sounds
        /// </summary>
        public void PauseAllSounds()
        {
	        _channelsToResume = new List<int>();
	        for (int i = 0; i < _fxASList.Length; i++)
	        {
		        if (_fxASList[i].isPlaying)
		        {
			        _channelsToResume.Add(i);
			        _fxASList[i].Pause();
		        }
	        }
        }
        
        /// <summary>
        /// Resume all sounds
        /// </summary>
        public void ResumeAllSounds()
        {
	        for (int i = 0; i < _channelsToResume.Count; i++)
	        {
		        if (_fxASList[_channelsToResume[i]].clip != null)
		        {
			        _fxASList[_channelsToResume[i]].Play();
		        }
	        }
        }
        
        /// <summary>
        /// Fade in sound
        /// </summary>
        public int FadeInSound(string sndId, float inSecs, SoundManagerCallback cbFn = null)
        {
	        return FadeInSound(Vector3.zero, sndId, inSecs, cbFn);
        }
        
        /// <summary>
        /// Fade in sound
        /// </summary>
        public int FadeInSound(Vector3 pos, string sndId, float inSecs, SoundManagerCallback cbFn = null)
        {
	        Debug.Assert(_soundManagerClips.ContainsKey(sndId));
	        var smc = _soundManagerClips[sndId];

	        var chnId = GetChannelIdx(smc);
	        
	        if (chnId != -1)
	        {
				var audSrc = _fxASList[chnId];

				// Stop the audiosource, just in case
				audSrc.Stop();
				
				// Set info on audiosource
				SetInfoOnAudioSource(chnId, smc, pos);
				
				// Set the volume to zero
		        audSrc.volume = 0f;
		        
		        // Start play with volume 0
		        audSrc.Play();

		        var fas = new FadeAudioSource
		        {
			        targetVolume = smc.volume,
			        initialVolume = 0f,
			        fadeInSecs = inSecs,
			        initialTime = Time.realtimeSinceStartup,
			        accumTime = 0f,
			        audioSrc = audSrc,
			        fnCb = cbFn
		        };

		        // Add the fader to the list
		        _listFaders.Add(fas);
		        
		        return chnId;
	        }
	        else
	        {
		        Debug.LogWarningFormat("[SoundManager] All audiosource are busy. Cannot play sound {0}", smc.name);
	        }

	        return -1;
        }
        
        /// <summary>
        /// Fade out the specified channel
        /// </summary>
        public void FadeOutSound(int chnId, float inSecs, SoundManagerCallback cbFn = null)
        {
	        var fas = new FadeAudioSource
	        {
		        initialTime = Time.realtimeSinceStartup,
		        accumTime = 0,
		        fadeInSecs = inSecs,
		        targetVolume = 0,
		        audioSrc = _fxASList[chnId],
		        initialVolume = _fxASList[chnId].volume,
		        fnCb = cbFn
	        };

	        _listFaders.Add(fas);
        }

        /// <summary>
        /// Copy the params from soundmanagerclip to the properties of the audiosource
        /// </summary>
        private void SetInfoOnAudioSource(int chnId, SoundManagerClip smc, Vector3 pos)
        {
	        _fxASList[chnId].clip = smc.clip;
	        _fxASList[chnId].loop = smc.loop;
	        _fxASList[chnId].volume = smc.volume;
	        _fxASList[chnId].pitch = smc.pitch;
	        _fxASList[chnId].panStereo = smc.stereoPan;
	        _fxASList[chnId].outputAudioMixerGroup = smc.mixerGroup;
	        _fxASList[chnId].transform.position = pos;
        }
        
        /// <summary>
        /// Play the specified soundmanagerclip on the specified audiosource
        /// </summary>
        private void PlayThisSoundOnSource(int chnId, SoundManagerClip smc, Vector3 pos)
        {
	        _fxASList[chnId].Stop();

	        SetInfoOnAudioSource(chnId, smc, pos);
	        
	        _fxASList[chnId].Play();
        }
        
        /// <summary>
        /// Returns a channeld idx to play a sound.
        /// Could be:
        /// 1. An empty channel (not yet used)
        /// 2. An IDLE channel
        /// 3. A busy channel but with less priority
        /// 4. A busy channel with the same priority
        /// 
        /// If there isn't a channel that satisfy these conditions, returns -1
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetChannelIdx(SoundManagerClip smc)
        {
        	for (int i = 0; i < channels; i++)
            {
	            if (_fxASList[i].clip == null || !_fxASList[i].isPlaying)
	            {
		            return i;
	            }
            }

        	// No audiosource idle. Find a busy audiosource with less priority than the new one
        	for (int i = 0; i < channels; i++)
        	{
        		if (_fxASList[i].clip != null && _listOfClipsInUse[i] != null && smc.priority > _listOfClipsInUse[i].priority)
                {
        			Debug.LogWarning("[SoundManager] Using a used channel with less priority.");
        			return i;
        		}
        	}

        	// Try something with the same priority
        	for (int i = 0; i < channels; i++)
        	{
        		if (_fxASList[i].clip != null && _listOfClipsInUse[i] != null && smc.priority == _listOfClipsInUse[i].priority)
        		{
	                Debug.LogWarning("[SoundManager] Using a used channel with the same priority.");
	                return i;
        		}
        	}

        	// Cannot find a suitable channel
        	return -1;
        }

        /// <summary>
        /// Set the volume of the specified mixer group. Volume is a normalized value between 0f and 1f
        /// </summary>
        public void SetVolume(string volumeExposedVarName, float volume)
        {
	        
	        audioMixer.SetFloat(volumeExposedVarName, Mathf.Lerp(minVolumeDb, maxVolumeDb, volume));
        }

        /// <summary>
        /// Return the volume from the specified mixer group. Volume is a normalized value between 0f and 1f
        /// </summary>
        public float GetVolume(string volumeExposedVarName)
        {
	        var volume = 0f;
	        audioMixer.GetFloat(volumeExposedVarName, out volume);
	        return (volume - minVolumeDb) / (maxVolumeDb - minVolumeDb);
        }
    }
}
