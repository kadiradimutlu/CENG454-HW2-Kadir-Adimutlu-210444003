// FlightController.cs
// CENG 454 HW1: Sky-High Prototype
// Author: Kadir ADIMUTLU | Student ID: 210444003

using UnityEngine;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed = 45f;
    [SerializeField] private float yawSpeed = 45f;
    [SerializeField] private float rollSpeed = 45f;
    [SerializeField] private float thrustSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        // Cache the Rigidbody component for performance
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Lock physics rotation to prevent the engine from fighting our custom flight logic
            rb.freezeRotation = true;
            Debug.Log("FlightController: Physics initialized and rotation locked.");
        }
        else
        {
            Debug.LogError("FlightController: Rigidbody missing on the aircraft!");
        }
    }

    void Update()
    {
        HandleRotation();
        HandleThrust();
    }

    private void HandleRotation()
    {
        // 1. Pitch: Vertical axis for nose up/down
        float pitchInput = Input.GetAxis("Vertical");
        float pitchAmount = pitchInput * pitchSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, pitchAmount);

        // 2. Yaw: Horizontal axis for steering left/right
        float yawInput = Input.GetAxis("Horizontal");
        float yawAmount = yawInput * yawSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yawAmount);

        // 3. Roll: Q and E keys for banking
        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput = 1f;
        else if (Input.GetKey(KeyCode.E)) rollInput = -1f;

        float rollAmount = rollInput * rollSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rollAmount);
    }

    private void HandleThrust()
    {
        // Thrust control via Spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            // Move forward along the local Z axis
            float thrustAmount = thrustSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * thrustAmount);
        }
    }
}