// Mono Framework
using System;
using System.Collections;

// Unity Framework
using UnityEngine;

namespace FrameLord.Sound
{
    public class FadeAudioSource
    {
        /// <summary>
        /// Target volume to go
        /// </summary>
        public float targetVolume;

        /// <summary>
        /// Initial volume
        /// </summary>
        public float initialVolume;

        /// <summary>
        /// Complete the fade in the specified seconds
        /// </summary>
        public float fadeInSecs;

        /// <summary>
        /// Initial time
        /// </summary>
        public float initialTime;

        /// <summary>
        /// Current delta time
        /// </summary>
        public float accumTime;

        /// <summary>
        /// The affected audiosource
        /// </summary>
        public AudioSource audioSrc;

        /// <summary>
        /// Callback function
        /// </summary>
        public SoundManagerCallback fnCb;
    }
}