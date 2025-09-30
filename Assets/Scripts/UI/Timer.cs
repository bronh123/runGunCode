using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountUpTimer : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI timerText;
    
    [Header("Timer Settings")]
    public bool autoStart = true;
    public bool showMilliseconds = false;
    
    [Header("Events")]
    public UnityEvent onTimerStart;
    public UnityEvent<float> onTimerUpdate; // Passes current time
    public UnityEvent onTimerStop;
    
    private float elapsedTime = 0f;
    private bool isRunning = false;
    
    void Start()
    {
        if (autoStart)
            StartTimer();
    }
    
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
            onTimerUpdate?.Invoke(elapsedTime);
        }
    }
    
    void UpdateTimerDisplay()
    {
        if (timerText == null) return;
        
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        
        if (hours > 0)
        {
            if (showMilliseconds)
                timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
            else
                timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        else if (minutes > 0)
        {
            if (showMilliseconds)
                timerText.text = $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
            else
                timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
        else
        {
            if (showMilliseconds)
                timerText.text = $"{seconds:D2}.{milliseconds:D3}";
            else
                timerText.text = $"{seconds:D2}";
        }
    }
    
    // Public control methods, might need for death, time pause, etc.
    public void StartTimer()
    {
        isRunning = true;
        onTimerStart?.Invoke();
    }
    
    public void StopTimer()
    {
        isRunning = false;
        onTimerStop?.Invoke();
    }
    
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }
    
    public void RestartTimer()
    {
        ResetTimer();
        StartTimer();
    }
    
    public void ToggleTimer()
    {
        if (isRunning)
            StopTimer();
        else
            StartTimer();
    }
    
    // Getters
    public float GetElapsedTime() => elapsedTime;
    public bool IsRunning() => isRunning;
    
    // Formatted time getters
    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        
        if (hours > 0)
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        else
            return $"{minutes:D2}:{seconds:D2}";
    }
}