using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float delay = 200f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip victorySFX;

    AudioSource audioSource; //same as 'this.gameObject.audioSource' or 'gameObject.audioSource'.
    
    private bool isControllable = true;


    /* 
     * 
     * 
     * The Invoke() and InvokeRepeating() methods in Unity cannot call static 
       methods due to how they work internally:
     * Reflection-based implementation - These methods use reflection to find and 
       call methods by name at runtime.
     * Instance binding requirement - Invoke is a MonoBehaviour method that looks for 
       methods within the specific instance that called it. It needs an object instance 
       to search within.

      * Method signature restrictions - Invoke can only call methods that:
        --> Take zero parameters
        --> Are instance methods (not static)
        --> Are part of the calling MonoBehaviour
      * 
      * 
      */

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {

        if (!isControllable) return;

        switch (collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("In Launchpad");
                break;
            case "Finish":
                Debug.Log("Reached Landing Pad");
                StartVictorySequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence() {
        // Disabling Controls once we crash
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    private void StartVictorySequence() {
        // Disabling Controls once we crash
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    private void LoadNextLevel() {
        isControllable = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
        
    }

    private void ReloadLevel() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; 
        // will be dynamic for other scenes as well
        SceneManager.LoadScene(sceneIndex);
    }
}
