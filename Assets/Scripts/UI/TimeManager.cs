using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // Singleton pattern
    public static TimerManager Instance { get; private set; }

    // Final time to be shared between scenes
    public float finalTime;

    private void Awake()
    {
        Debug.Log("TimerManager Awake");
        // Singleton pattern setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set the final time from other scripts
    public void SetFinalTime(float time)
    {
        finalTime = time;
    }
}