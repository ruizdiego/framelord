using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FrameLord.FSM
{
    public class State : MonoBehaviour
    {
        // Reference to the state manager
        protected StateManager _sm;

        private StateConnection[] _connections;

        /// <summary>
        /// Unity Awake Method
        /// </summary>
        void Awake()
        {
            GetConnections();
        }

        private void GetConnections()
        {
            if (_connections == null) _connections = GetComponents<StateConnection>();
        }
        
        internal void InitState(StateManager sm)
        {
            _sm = sm;

            if (_connections == null) GetConnections();
            
            if (_connections != null)
            {
                for (int i = 0; i < _connections.Length; i++)
                {
                    _connections[i].Init(sm);
                }
            }

            OnInitState();
        }

        internal void EnterState()
        {
            if (_connections == null) GetConnections();
            
            if (_connections != null)
            {
                for (int i = 0; i < _connections.Length; i++)
                {
                    _connections[i].Reset();
                }
            }

            OnEnterState();
        }

        internal void LeaveState()
        {
            OnLeaveState();
        }

        /// <summary>
        /// Returns the state manager
        /// </summary>
        public StateManager GetStateManager()
        {
            return _sm;
        }
        
        internal void CheckConditions()
        {
            if (_connections == null) GetConnections();
            
            if (_connections != null)
            {
                for (int i = 0; i < _connections.Length; i++)
                {
                    _connections[i].CheckCondition();

                    if (_connections[i].IsFinished())
                    {
                        _sm.SwitchTo(_connections[i].connectTo);
                        return;
                    }
                }
            }
        }
        
        protected virtual void OnInitState()
        {
        }
    
        protected virtual void OnEnterState()
        {
        }
    
        protected virtual void OnLeaveState()
        {
        }
    }
    
}