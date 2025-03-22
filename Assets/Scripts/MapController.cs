using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public void GoToGameScene1(){
        SceneManager.LoadScene("GameScene1");
    }
    
}
