using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action SwingsStarted;
    public event Action Catapulted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwingsStarted?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Catapulted?.Invoke();
        }
    }
}
