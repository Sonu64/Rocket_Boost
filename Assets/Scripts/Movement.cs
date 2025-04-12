using UnityEngine;
using UnityEngine.InputSystem; // Input System namespace

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 100f;
    Rigidbody rigidbody;

    /**
     OnEnable() is called every time a script or GameObject is enabled. 
     This is important because you often want input actions to be re-enabled
     if the object is reactivated at runtime (e.g., switching UI screens, 
     toggling objects on/off).
    */

    private void OnEnable() {
        thrust.Enable();
    }

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (thrust.IsPressed()) {
            // Vector3.up is a Vector3 = (0,1,0), Give very high thrustStrength
            // as fixedDeltaTime is much lesser that DeltaTime
            rigidbody.AddRelativeForce(Vector3.up*Time.fixedDeltaTime*thrustStrength);
        }
    }
}
