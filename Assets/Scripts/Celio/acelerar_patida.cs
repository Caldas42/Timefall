using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    private bool isFast = false;

    public void ToggleSpeed()
    {
        if (isFast)
        {
            Time.timeScale = 1f;
            isFast = false;
        }
        else
        {
            Time.timeScale = 2f;
            isFast = true;
        }
    }
}
