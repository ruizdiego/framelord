
// Unity Framework
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    private enum State
    {
        Flip,
        Flop
    }
    
    // Blink Off Time
    public float blinkOffTime = 1f;

    // Blink On Time
    public float blinkOnTime = 1f;

    // Time accumulator
    private float _accumTime;

    // State
    private State _state;

    // Reference the maskable graphic item
    private MaskableGraphic _ref;
    
    /// <summary>
    /// Unity Start Method
    /// </summary>
    void Start()
    {
        _state = State.Flip;
        _ref = GetComponent<MaskableGraphic>();
    }

    /// <summary>
    /// Unity Update Method
    /// </summary>
    void Update()
    {
        _accumTime += Time.deltaTime;
        switch (_state)
        {
            case State.Flip:
                if (_accumTime > blinkOnTime)
                {
                    _accumTime %= blinkOnTime;
                    _ref.enabled = false;
                    _state = State.Flop;
                }
                break;
            
            case State.Flop:
                if (_accumTime > blinkOffTime)
                {
                    _accumTime %= blinkOffTime;
                    _ref.enabled = true;
                    _state = State.Flip;
                }
                break;
        }
    }
}
