using UnityEngine;

public class LandingArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FlightExamManager examManager = FindObjectOfType<FlightExamManager>();
            if (examManager != null)
            {
                examManager.ReportLanding();
            }
        }
    }
}