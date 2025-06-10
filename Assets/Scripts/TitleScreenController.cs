using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public void CallScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
}
