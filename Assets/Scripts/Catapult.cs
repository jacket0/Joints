using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class Catapult : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private Vector3 _originalPosition;
    [SerializeField] private Vector3 _topPosition;

    private SpringJoint _springJoint;
    private bool _isReady = true;

    private void Awake()
    {
        _springJoint = GetComponent<SpringJoint>();
        _springJoint.autoConfigureConnectedAnchor = false;
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
            _springJoint.connectedAnchor = _topPosition;
        }
        else
        {
            _springJoint.connectedAnchor = _originalPosition;
        }
    }    
}
