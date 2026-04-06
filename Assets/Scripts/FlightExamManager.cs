// FlightExamManager.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Kadir ADIMUTLU | Student ID: 210444003

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip warningClip;
    [SerializeField] private AudioSource victoryAudioSource;
    
    private bool hasTakenOff = false;
    private bool threatCleared = false;
    private bool missionComplete = false;
    private bool isOutOfBounds = false;

    private void Start()
    {
        if (statusText != null)
        {
            statusText.text = "MISSION: Take off, evade the threat, and land safely.\nHold [SPACE] to thrust.";
            statusText.color = Color.white;
        }
    }

    private void Update()
    {
        if (missionComplete && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public bool HasTakenOff() => hasTakenOff;
    public bool IsThreatCleared() => threatCleared;
    public bool IsMissionComplete() => missionComplete;

    public void ReportTakeoff()
    {
        if (!hasTakenOff)
        {
            hasTakenOff = true;
            if (statusText != null && !isOutOfBounds) statusText.text = ""; 
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
                statusText.text = "Mission Accomplished!\nPress 'R' to Restart";
                statusText.color = Color.green;
            }
            Debug.Log("Mission State: Success. Aircraft landed safely after clearing the threat.");

            // ZAFER SESİ BURADA ÇALIYOR
            if (victoryAudioSource != null && !victoryAudioSource.isPlaying)
            {
                victoryAudioSource.Play();
            }
        }
        else
        {
            if (statusText != null)
            {
                statusText.text = "Mission Failed!\nPress 'R' to Restart";
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
            statusText.text = "Aircraft Crashed! Mission Failed.\nPress 'R' to Restart";
            statusText.color = Color.red;
        }
        Debug.Log("Mission State: Failed. Aircraft crashed into terrain.");
    }

    public void ReportMissileHit()
    {
        if (missionComplete) return;

        missionComplete = true;
        StopWarningAudio();

        if (statusText != null)
        {
            statusText.text = "Aircraft Destroyed! Mission Failed.\nPress 'R' to Restart";
            statusText.color = Color.red;
        }
        Debug.Log("Mission State: Failed. Aircraft destroyed by missile.");
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
            if (!hasTakenOff)
            {
                statusText.text = "MISSION: Take off, evade the threat and land safely.\nHold [SPACE] to thrust.";
                statusText.color = Color.white;
            }
            else
            {
                statusText.text = ""; 
            }
        }
    }
}