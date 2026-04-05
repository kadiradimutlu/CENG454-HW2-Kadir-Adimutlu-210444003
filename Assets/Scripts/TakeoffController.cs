// TakeoffController.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Kadir ADIMUTLU | Student ID: 210444003

using UnityEngine;

public class TakeoffController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            examManager.ReportTakeoff();
        }
    }
}