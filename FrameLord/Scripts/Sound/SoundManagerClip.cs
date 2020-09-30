
// Unity Framework
using UnityEngine;
using UnityEngine.Audio;

namespace FrameLord.Sound
{
    [ExecuteInEditMode]
    public class SoundManagerClip : MonoBehaviour
    {
        // Audioclip reference
        public AudioClip clip;

        // Audio mixer group
        public AudioMixerGroup mixerGroup;

        // Loop
        public bool loop = false;
        
        // Volume
        [Range (0f,1f)]
        public float volume = 1f;
        
        // Priority
        [Range (0,255)]
        public int priority = 128;
        
        // Pitch
        [Range (-3,3)]
        public float pitch = 1f;

        // Stereo Pan
        [Range (-1f,1f)]
        public float stereoPan = 0f;

    }
}