using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Catapult : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private float _downTargetPosition = -20;
    [SerializeField] private float _topTargetPosition = 120;

    private HingeJoint _joint;
    private bool _isReady = true;

    private void Awake()
    {
        _joint = GetComponent<HingeJoint>();
    }

    private void OnEnable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.SwingsStarted += Catapulting;
        }
    }

    private void OnDisable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.SwingsStarted -= Catapulting;
        }
    }

    private void Catapulting()
    {
        if (_isReady)
        {
            ChangeAngle(_topTargetPosition);
            _isReady = false;
        }
        else
        {
            ChangeAngle(_downTargetPosition);
            _isReady = true;
        }
    }    

    private void ChangeAngle(float angle)
    {
        var j = _joint.spring;
        j.targetPosition = angle;
        _joint.spring = j;
    }
}
