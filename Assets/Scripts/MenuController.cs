using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoToMap(){
        SceneManager.LoadScene("MapScene");
    }

}
