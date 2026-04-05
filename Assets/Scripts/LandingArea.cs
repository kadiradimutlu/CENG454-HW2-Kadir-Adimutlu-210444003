// LandingArea.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Kadir ADIMUTLU | Student ID: 210444003

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