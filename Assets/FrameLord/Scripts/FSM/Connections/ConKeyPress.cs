using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameLord
{
    public class ConKeyPress : FrameLord.StateConnection
    {
        public KeyCode keyToPress;
        
        protected override void OnCheckCondition()
        {
            _isFinished = UnityEngine.Input.GetKeyDown(keyToPress);
        }
    }
}