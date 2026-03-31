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