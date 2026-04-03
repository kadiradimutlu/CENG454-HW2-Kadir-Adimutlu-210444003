using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip warningClip;
    
    private bool hasTakenOff = false;
    private bool threatCleared = false;
    private bool missionComplete = false;

    public bool HasTakenOff() => hasTakenOff;
    public bool IsThreatCleared() => threatCleared;
    public bool IsMissionComplete() => missionComplete;

    public void ReportTakeoff()
    {
        if (!hasTakenOff)
        {
            hasTakenOff = true;
            Debug.Log("Mission State: Takeoff reported.");
        }
    }

    public void EnterDangerZone()
    {
        if (statusText != null)
        {
            statusText.text = "Entered a Dangerous Zone!";
            statusText.color = Color.red;
        }

        if (audioSource != null && warningClip != null)
        {
            audioSource.clip = warningClip;
            audioSource.loop = true;
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    public void ExitDangerZone()
    {
        if (statusText != null) statusText.text = ""; 
        threatCleared = true;
        
        StopWarningAudio();
    }

    public void StopWarningAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}