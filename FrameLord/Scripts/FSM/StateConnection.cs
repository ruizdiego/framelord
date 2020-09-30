using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameLord.FSM
{
    public class StateConnection : MonoBehaviour
    {
        public bool isEnabled = true;

        // Connect to the following state
        public State connectTo;

        // Reference to the state manager
        protected StateManager _sm;

        // Is the condition flagged?
        protected bool _isFinished = false;

        internal void Init(StateManager sm)
        {
            _sm = sm;
            OnInit();
        }

        internal void CheckCondition()
        {
            if (isEnabled) OnCheckCondition();
        }

        internal void Reset()
        {
            _isFinished = false;
            OnReset();
        }

        internal bool IsFinished()
        {
            if (_isFinished) OnFinished();
            return _isFinished;
        }

        protected virtual void OnInit()
        {
        }
        
        protected virtual void OnCheckCondition()
        {
        }
        
        protected virtual void OnReset()
        {
        }
        protected virtual void OnFinished()
        {
        }
        
    }
}