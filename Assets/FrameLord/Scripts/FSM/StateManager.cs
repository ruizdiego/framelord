using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameLord
{
    public class StateManager : MonoBehaviour
    {
        // Initial state
        public State initialState;

        private State[] _states;
        
        // Current state
        private State _currentState;

        /// <summary>
        /// Unity Awake Method
        /// </summary>
        void Awake()
        {
            DontDestroyOnLoad(this);
            _states = GetComponentsInChildren<State>(true);
        }

        /// <summary>
        /// Unity Start Method
        /// </summary>
        void Start()
        {
            State newState = null;
                        
            for (int i = 0; i < _states.Length; i++)
            {
                if (_states[i] == initialState)
                {
                    newState = _states[i];
                }
                else
                {
                    _states[i].gameObject.SetActive(false);
                }

                _states[i].InitState(this);
            }

            SwitchTo(newState);
        }

        /// <summary>
        /// Unity Update Method
        /// </summary>
        void Update()
        {
            if (_currentState != null)
            {
                _currentState.CheckConditions();
            }
        }

        /// <summary>
        /// Switch to the specified state
        /// </summary>
        public void SwitchTo(State newState)
        {
            // Prevents to enter and leave the same state
            if (_currentState == newState) return;

            if (_currentState != null)
            {
                _currentState.LeaveState();
                _currentState.gameObject.SetActive(false);
            }

            _currentState = newState;

            if (_currentState != null)
            {
                _currentState.gameObject.SetActive(true);
                _currentState.EnterState();
            }
        }

        /// <summary>
        /// Returns the current state
        /// </summary>
        public State GetCurrentState()
        {
            return _currentState;
        }

        /// <summary>
        /// Returns the current state name or empty if there isn't any current state
        /// </summary>
        public string GetCurrentStateId()
        {
            if (_currentState != null)
            {
                return _currentState.name;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}