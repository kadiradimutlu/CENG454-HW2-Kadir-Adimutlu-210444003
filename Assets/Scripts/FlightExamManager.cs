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
    private bool isOutOfBounds = false;

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
        if (missionComplete) return;

        if (statusText != null && !isOutOfBounds)
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
        if (missionComplete) return;

        if (statusText != null && !isOutOfBounds) statusText.text = ""; 
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

    public void ReportLanding()
    {
        if (missionComplete) return;

        missionComplete = true;
        StopWarningAudio(); 

        if (hasTakenOff && threatCleared)
        {
            if (statusText != null)
            {
                statusText.text = "Mission Accomplished!";
                statusText.color = Color.green;
            }
            Debug.Log("Mission State: Success. Aircraft landed safely after clearing the threat.");
        }
        else
        {
            if (statusText != null)
            {
                statusText.text = "Mission Failed!";
                statusText.color = Color.red;
            }
            Debug.Log("Mission State: Failed. Invalid landing or threat not cleared.");
        }
    }

    public void ReportCrash()
    {
        if (missionComplete) return;

        missionComplete = true;
        StopWarningAudio();

        if (statusText != null)
        {
            statusText.text = "Aircraft Crashed! Mission Failed.";
            statusText.color = Color.red;
        }
        Debug.Log("Mission State: Failed. Aircraft crashed into terrain.");
    }

    public void ShowBoundaryWarning()
    {
        if (missionComplete) return;
        
        isOutOfBounds = true;
        if (statusText != null)
        {
            statusText.text = "WARNING: Return to Mission Area!";
            statusText.color = new Color(1f, 0.5f, 0f); 
        }
    }

    public void HideBoundaryWarning()
    {
        if (missionComplete) return;
        
        isOutOfBounds = false;
        if (statusText != null)
        {
            statusText.text = ""; 
        }
    }
}