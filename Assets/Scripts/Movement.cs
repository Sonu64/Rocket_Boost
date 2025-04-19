using UnityEngine;
using UnityEngine.InputSystem;



public class Movement : MonoBehaviour
{
    // SERIALIZED VARIABLES at the Top
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 60f;
    [SerializeField] AudioClip engineThrustSFX;
    [SerializeField] ParticleSystem mainBoosterParticles; 
    [SerializeField] ParticleSystem leftBoosterParticles, rightBoosterParticles;

    
    // CACHED REFERENCE VARIABLES Come next
    Rigidbody rigidBody;
    AudioSource audioSource;


    // STATE VARIABLES Come next


    // CALLBACKS like OnEnable(), Start(), OnCollisionEnter(), etc. comes next
    /*
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
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        ProcessThrust();
        ProcessRotation();
    }


    // Then finally we declare OUR OWN METHODS.
    private void ProcessThrust() {
        if (thrust.IsPressed()) {
            // Vector3.up is a Vector3 = (0,1,0), Give very high thrustStrength
            // as fixedDeltaTime is much lesser that DeltaTime
            rigidBody.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
            if (!mainBoosterParticles.isPlaying)
                mainBoosterParticles.Play();
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(engineThrustSFX);
        } else {
            mainBoosterParticles.Stop();
            audioSource.Stop();
        }
    }

    private void ProcessRotation() {
        float rotationInput = (rotation.ReadValue<float>());
        
        // if-elseIf is better to ensure mutual Exclusiveness such that both doesn't happen
        // at the same time at any condition. Shouldn't happen though.

        if (rotationInput > 0) { // +1, returned by hitting D
            ApplyRotation(-rotationStrength);
            if (!rightBoosterParticles.isPlaying) {
                leftBoosterParticles.Stop(); // Just to avoid both particles playing at the same time.
                rightBoosterParticles.Play();
            }
                
            /* We pass NEGATIVE of rotationStrength here due to the alignment of the Rocket in Space,
             Increasing the value of Z actually rotates the rocket LEFT. We want D to return +1
             and rotate to RIGHT, so we passed NEGATIVE Rotation Strength here to decrease Z.
             transforms to Vector3.forward * (-rotationStrength) * Time.fixedDeltaTime for (0,0,-1) */
        } else if (rotationInput < 0) { // -1 returned by hitting A
            ApplyRotation(rotationStrength);
            if(!leftBoosterParticles.isPlaying) {
                rightBoosterParticles.Stop(); 
                leftBoosterParticles.Play();
            }
                
            //transforms to Vector3.forward * (rotationStrength) * Time.fixedDeltaTime for (0,0,+1)
        }else {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
        }
    }



    private void ApplyRotation(float currentFrameRotation) {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * currentFrameRotation * Time.fixedDeltaTime);
        rigidBody.freezeRotation = false;
    }

}
