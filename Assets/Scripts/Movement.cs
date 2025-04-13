using UnityEngine;
using UnityEngine.InputSystem; // Input System namespace

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 60f;

    Rigidbody rigidbody;

    /**
     OnEnable() is called every time a script or GameObject is enabled. 
     This is important because you often want input actions to be re-enabled
     if the object is reactivated at runtime (e.g., switching UI screens, 
     toggling objects on/off).
    */

    private void OnEnable() {
        thrust.Enable();
        rotation.Enable();
    }

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust() {
        if (thrust.IsPressed()) {
            // Vector3.up is a Vector3 = (0,1,0), Give very high thrustStrength
            // as fixedDeltaTime is much lesser that DeltaTime
            rigidbody.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
        }
    }

    private void ProcessRotation() {
        float rotationInput = (rotation.ReadValue<float>());
        
        // if-elseIf is better to ensure mutual Exclusiveness such that both doesn't happen
        // at the same time at any condition. Shouldn't happen though.

        if (rotationInput < 0) { // -1 is for Right Rotation ABOUT THE Z-AXIS
            ApplyRotation(rotationStrength);
            //transforms to Vector3.forward * rotationStrength * Time.fixedDeltaTime for (0,0,1)
        } else if (rotationInput > 0) { // +1 is for Left Rotation ABOUT THE Z-AXIS
            ApplyRotation(-rotationStrength);
            //transforms to Vector3.forward * (-rotationStrength) * Time.fixedDeltaTime for (0,0,-1)
        }
    }

    private void ApplyRotation(float currentFrameRotation) {
        transform.Rotate(Vector3.forward * currentFrameRotation * Time.fixedDeltaTime);
    }
}
