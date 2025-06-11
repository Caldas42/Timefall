using UnityEngine;
using TMPro;

public class ScreenModeManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown screenModeDropdown;
    private const string ScreenModeKey = "ScreenMode";

    void Start()
    {
        int savedScreenModeIndex = PlayerPrefs.GetInt(ScreenModeKey, 0);

        screenModeDropdown.value = savedScreenModeIndex;

        if ((savedScreenModeIndex == 0 && !Screen.fullScreen) || (savedScreenModeIndex == 1 && Screen.fullScreen))
        {
            ApplyScreenMode(savedScreenModeIndex, false);
        }

        screenModeDropdown.onValueChanged.AddListener(OnScreenModeSelected);
    }

    void OnScreenModeSelected(int index)
    {
        ApplyScreenMode(index, true);
    }

    void ApplyScreenMode(int index, bool savePrefs)
    {
        if (index == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
        }
        else if (index == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }

        if (savePrefs)
        {
            PlayerPrefs.SetInt(ScreenModeKey, index);
            PlayerPrefs.Save();
        }
    }
}
