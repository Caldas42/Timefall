using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public string initialSceneName = "Title Screen";

    public void ResetToStart()
    {
        SceneManager.LoadScene(initialSceneName);
    }
}
