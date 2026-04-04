using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float detonationDistance = 3f; 
    [SerializeField] private AudioClip explosionClip;

    private Transform target;
    private bool isDestroyed = false;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target == null || isDestroyed) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        
        if (distanceToTarget <= detonationDistance)
        {
            Detonate();
            return;
        }

        Vector3 direction = target.position - transform.position;
        
        if (direction.sqrMagnitude > 0.01f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;

        if (other.CompareTag("Player"))
        {
            Detonate();
        }
    }

    private void Detonate()
    {
        isDestroyed = true;
        Debug.Log("Threat System: Missile collided with player. Aircraft destroyed.");
        
        // Stop warning and launch audios to prevent overlapping with the explosion
        FlightExamManager examManager = FindObjectOfType<FlightExamManager>();
        if (examManager != null)
        {
            examManager.StopWarningAudio();
        }

        MissileLauncher launcher = FindObjectOfType<MissileLauncher>();
        if (launcher != null)
        {
            launcher.StopLaunchAudio();
        }

        if (explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, transform.position);
        }

        Destroy(gameObject);
    }
}