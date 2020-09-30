// Unity Framework
using UnityEngine;

namespace FrameLord.FSM
{
    public class ConTimer : FrameLord.FSM.StateConnection
    {
        public float time;

        private float _accumTime;
        
        protected override void OnCheckCondition()
        {
            _accumTime += Time.deltaTime;
            _isFinished = (_accumTime >= time);
        }
        
        protected override void OnReset()
        {
            _accumTime = 0f;
        }
    }
}