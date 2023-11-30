using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        // Display the final time from the TimerManager
        if (timeText != null)
        {
            timeText.text = "Final Time: " + Timer.Instance.finalTime.ToString("0.00") + " seconds";
        }
        else
        {
            Debug.LogWarning("UI Text reference is missing!");
        }
    }
}