using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Deze functie laadt de volgende scene
    public void LoadNextSceneMethod()
    {
        // Haal de huidige scene index op
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Laad de volgende scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}