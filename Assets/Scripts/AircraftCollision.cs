using UnityEngine;

public class AircraftCollision : MonoBehaviour
{
    private FlightExamManager examManager;

    private void Start()
    {
        examManager = FindObjectOfType<FlightExamManager>();
    }

    // Detects physical crashes into the ground/mountains
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if (examManager != null)
            {
                examManager.ReportCrash();
            }
        }
    }

    // Detects returning to the safe flight zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            if (examManager != null)
            {
                examManager.HideBoundaryWarning();
            }
        }
    }

    // Detects leaving the safe flight zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            if (examManager != null)
            {
                examManager.ShowBoundaryWarning();
            }
        }
    }
}