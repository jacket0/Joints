using UnityEngine;

[RequireComponent(typeof(HingeJoint), typeof(Rigidbody))]
public class Swings : MonoBehaviour
{
    [SerializeField] private float _force = 500f;
    [SerializeField] private InputHandler _inputHandler;

    private HingeJoint _hingeJoint;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _hingeJoint = GetComponent<HingeJoint>();
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

    private void StartSwinging()
    {
        _rigidBody.AddForce(_force * _hingeJoint.axis);
    }
}
