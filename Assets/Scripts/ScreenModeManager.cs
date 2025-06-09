using UnityEngine;
using TMPro;

public class ScreenModeSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        if (dropdown == null)
        {
            Debug.LogError("Dropdown não está atribuído no Inspector!");
            return;
        }

        dropdown.onValueChanged.AddListener(OnScreenModeChanged);
        OnScreenModeChanged(dropdown.value);
    }

    void OnScreenModeChanged(int index)
    {
        if (index == 0)
        {
            Debug.Log("Modo Janela selecionado");
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
            Camera.main.backgroundColor = Color.red;
        }
        else if (index == 1)
        {
            Debug.Log("Tela Cheia selecionada");
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
            Camera.main.backgroundColor = Color.green;
        }
        else
        {
            Debug.LogWarning("Índice desconhecido no Dropdown: " + index);
        }

        Debug.Log($"Estado fullscreen: {Screen.fullScreen}");
        Debug.Log($"Resolução atual: {Screen.width} x {Screen.height}");
    }
}
