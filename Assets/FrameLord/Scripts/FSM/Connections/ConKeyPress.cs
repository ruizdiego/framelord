using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameLord.FSM
{
    public class ConKeyPress : FrameLord.FSM.StateConnection
    {
        public KeyCode keyToPress;
        
        protected override void OnCheckCondition()
        {
            _isFinished = UnityEngine.Input.GetKeyDown(keyToPress);
        }
    }
}