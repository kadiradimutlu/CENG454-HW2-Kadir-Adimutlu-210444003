using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    
    private bool hasTakenOff = false;
    private bool threatCleared = false;
    private bool missionComplete = false;

    public void ReportTakeoff()
    {
        if (!hasTakenOff)
        {
            hasTakenOff = true;
            Debug.Log("Mission State: Aircraft has successfully taken off.");
        }
    }

    public void EnterDangerZone()
    {
        if (statusText != null)
        {
            statusText.text = "Entered a Dangerous Zone!";
            statusText.color = Color.red;
        }
    }

    public void ExitDangerZone()
    {
        if (statusText != null)
        {
            statusText.text = ""; 
        }
        
        threatCleared = true;
    }
}