using UnityEngine;
using TMPro;

public class ScreenModeSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private int pendingScreenModeIndex = -1;
    private const string ScreenModeKey = "ScreenMode";

    void Start()
    {
        if (dropdown == null)
        {
            Debug.LogError("Dropdown não está atribuído no Inspector!");
            return;
        }

        int savedScreenModeIndex = PlayerPrefs.GetInt(ScreenModeKey, 1);

        dropdown.value = savedScreenModeIndex;

        if ((savedScreenModeIndex == 0 && Screen.fullScreen) || (savedScreenModeIndex == 1 && !Screen.fullScreen))
        {
            ApplyScreenMode(savedScreenModeIndex, false);
        }
        else
        {
            UpdateBackgroundColor(savedScreenModeIndex);
        }

        dropdown.onValueChanged.AddListener(OnScreenModeSelected);
    }

    void OnScreenModeSelected(int index)
    {
        pendingScreenModeIndex = index;
        ApplyScreenMode(index, true); // Playerprefs recebe True
    }

    void ApplyScreenMode(int index, bool savePrefs)
    {
        if (index == 0)
        {
            Debug.Log("Modo Janela selecionado");
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }
        else if (index == 1)
        {
            Debug.Log("Tela Cheia selecionada");
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
        }
        else
        {
            Debug.LogWarning("Índice desconhecido no Dropdown: " + index);
        }

        Debug.Log($"Estado fullscreen: {Screen.fullScreen}");
        Debug.Log($"Resolução atual: {Screen.width} x {Screen.height}");

        UpdateBackgroundColor(index);

        if (savePrefs)
        {
            PlayerPrefs.SetInt(ScreenModeKey, index);
            PlayerPrefs.Save();
        }
    }

    void UpdateBackgroundColor(int index)
    {
        if (index == 0)
        {
            Camera.main.backgroundColor = Color.red;
        }
        else if (index == 1)
        {
            Camera.main.backgroundColor = Color.green;
        }
    }
}
