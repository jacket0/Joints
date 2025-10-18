using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Swings : MonoBehaviour
{
    [SerializeField] private float _force = 500f;
    [SerializeField] private float _velocity = 100f;
    [SerializeField] private bool _useMotor = true;
    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private float _minAngle = -60f;
    [SerializeField] private float _maxAngle = 60f;
    [SerializeField] private float _angleTolerance = 0.1f;

    private HingeJoint _hingeJoint;
    private Rigidbody _rigidBody;
    private bool _isSwinging;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
        _hingeJoint.useLimits = true;

        JointLimits limits = _hingeJoint.limits;
        limits.max = _maxAngle;
        limits.min = _minAngle;
        _hingeJoint.limits = limits;

        _isSwinging = false;
    }

    private void OnEnable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.SwingsStarted += StartSwinging;
        }
    }

    private void OnDisable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.SwingsStarted -= StartSwinging;
        }
    }

    private void Update()
    {
        if (!_isSwinging) 
            return;

        if (Mathf.Abs(_hingeJoint.angle - _maxAngle) <= _angleTolerance || Mathf.Abs(_hingeJoint.angle - _minAngle) <= _angleTolerance)
        {
            _velocity *= -1f;
            UpdateMotor();
        }
    }

    private void StartSwinging()
    {
        _isSwinging = true;
        _hingeJoint.useMotor = _useMotor;
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        if (!_useMotor) 
            return;

        JointMotor motor = _hingeJoint.motor;
        motor.force = _force;
        motor.targetVelocity = _velocity;
        _hingeJoint.motor = motor;
    }
}