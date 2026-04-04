using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;
    [SerializeField] private float missileDelay = 9f;

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            examManager.EnterDangerZone();
            
            // Start the 5-second countdown
            activeCountdown = StartCoroutine(LaunchCountdown(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            examManager.ExitDangerZone();
            
            // Cancel countdown if player exits early
            if (activeCountdown != null)
            {
                StopCoroutine(activeCountdown);
                activeCountdown = null;
                Debug.Log("Threat System: Launch countdown cancelled due to early exit.");
            }

            // Destroy missile if it is already flying
            if (missileLauncher != null)
            {
                missileLauncher.DestroyActiveMissile();
            }
        }
    }

    private IEnumerator LaunchCountdown(Transform targetTransform)
    {
        Debug.Log("Threat System: Missile launch countdown started (5 seconds)...");
        yield return new WaitForSeconds(missileDelay);
        
        if (missileLauncher != null)
        {
            missileLauncher.Launch(targetTransform);
        }
    }
}