// .NET Framework
using System.Collections.Generic;

// Unity Framework
using UnityEngine;


namespace FrameLord.Sound
{
    public class SoundManagerExtension : MonoBehaviour
    {
        private List<SoundManagerClip> _listOfSoundManagerClips;
        
        /// <summary>
        /// Unity Awake Method
        /// </summary>
        void Awake()
        {
            GetAllSoundManagerClips();

            SoundManager.Instance.AddSoundManagerClips(_listOfSoundManagerClips);
        }

        /// <summary>
        /// OnDestroy Method
        /// </summary>
        void OnDestroy()
        {
            SoundManager.Instance.RemoveSoundManagerClips(_listOfSoundManagerClips);
        }
        
        /// <summary>
        /// Get all the SoundManagerClips
        /// </summary>
        private void GetAllSoundManagerClips()
        {
            _listOfSoundManagerClips = new List<SoundManagerClip>();
	        
            var clips = GetComponentsInChildren<SoundManagerClip>();
            for (int i = 0; i < clips.Length; i++)
            {
                _listOfSoundManagerClips.Add(clips[i]);
            }
        }
    }
}
