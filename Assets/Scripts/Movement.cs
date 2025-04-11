using UnityEngine;
using UnityEngine.InputSystem; // Input System namespace

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;

    /**
     OnEnable() is called every time a script or GameObject is enabled. 
     This is important because you often want input actions to be re-enabled
     if the object is reactivated at runtime (e.g., switching UI screens, 
     toggling objects on/off).
    */

    private void OnEnable() {
        thrust.Enable();
    }

    private void Update() {
        if (thrust.IsPressed()) {
            Debug.Log("You Pressed Thrust key !");
        }
    }
}
