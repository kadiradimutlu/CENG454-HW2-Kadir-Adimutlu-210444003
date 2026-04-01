using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform launchPoint;
    
    private GameObject activeMissile;

    public GameObject Launch(Transform target)
    {
        if (missilePrefab != null && launchPoint != null)
        {
            activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
            Debug.Log("Threat System: Missile Launched!");
            
            // Homing script assignment will be added in Day 4
            return activeMissile;
        }
        return null;
    }

    public void DestroyActiveMissile()
    {
        if (activeMissile != null)
        {
            Destroy(activeMissile);
            Debug.Log("Threat System: Active missile destroyed.");
        }
    }
}