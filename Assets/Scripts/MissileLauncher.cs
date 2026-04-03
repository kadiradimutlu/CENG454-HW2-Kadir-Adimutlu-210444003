using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip launchClip;
    
    private GameObject activeMissile;

    public GameObject Launch(Transform target)
    {
        if (missilePrefab != null && launchPoint != null)
        {
            activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
            Debug.Log("Threat System: Missile launched.");
            
            // Trigger the launch audio playback
            if (audioSource != null && launchClip != null)
            {
                audioSource.clip = launchClip;
                audioSource.Play();
            }
            
            MissileHoming homingComponent = activeMissile.GetComponent<MissileHoming>();
            if (homingComponent != null)
            {
                homingComponent.SetTarget(target);
            }
            
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

    public void StopLaunchAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}