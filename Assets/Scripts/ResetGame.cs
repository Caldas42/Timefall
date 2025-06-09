using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public string initialSceneName = "Menu";

    public void ResetToStart()
    {
        SceneManager.LoadScene(initialSceneName);
    }
}
