using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Singleton pattern
    public static Timer Instance { get; private set; }

    // Final time to be shared between scenes
    public float finalTime;

    public TextMeshProUGUI timerText;
    public float currentTime;
    public bool countDown;
    public bool hasLimit;
    public float timerLimit;

    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
            
            finalTime = currentTime;
            
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.00");
    }
}