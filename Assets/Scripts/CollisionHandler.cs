using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Fuel":
                Debug.Log("Fuel Hit");
                break;
            case "Friendly":
                Debug.Log("In Launchpad");
                break;
            case "Finish":
                Debug.Log("Reached Landing Pad");
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    private void ReloadLevel() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // will be dynamic for other scenes as well
        SceneManager.LoadScene(sceneIndex);
    }
}
