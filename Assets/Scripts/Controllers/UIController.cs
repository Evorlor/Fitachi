using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Loads the specified scene
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}