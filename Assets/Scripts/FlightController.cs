// FlightController.cs
// CENG 454 - HW2 Midterm: Sky-High Prototype II
// Author: Kadir ADIMUTLU | Student ID: 210444003

using UnityEngine;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed = 45f;
    [SerializeField] private float yawSpeed = 45f;
    [SerializeField] private float rollSpeed = 45f;
    [SerializeField] private float thrustSpeed = 7f;
    [SerializeField] private float gravityForce = 0.6f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioClip engineClip;
    [SerializeField] private float idlePitch = 0.8f;
    [SerializeField] private float thrustPitch = 1.2f;

    private Rigidbody rb;
    private FlightExamManager examManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        examManager = FindObjectOfType<FlightExamManager>();

        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.useGravity = false; 
            Debug.Log("FlightController: Physics initialized and rotation locked.");
        }
        else
        {
            Debug.LogError("FlightController: Rigidbody missing on the aircraft!");
        }

        InitializeAudio();
    }

    void Update()
    {
        if (examManager != null && examManager.IsMissionComplete())
        {
            if (engineAudio != null && engineAudio.isPlaying)
            {
                engineAudio.Stop();
            }
            return;
        }

        HandleRotation();
        HandleThrust();
        ApplyCustomGravity();
    }

    private void InitializeAudio()
    {
        if (engineAudio != null && engineClip != null)
        {
            engineAudio.clip = engineClip;
            engineAudio.loop = true;
            engineAudio.pitch = idlePitch;
            engineAudio.Play();
        }
    }

    private void HandleRotation()
    {
        float pitchInput = Input.GetAxis("Vertical");
        float pitchAmount = pitchInput * pitchSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, pitchAmount);

        float yawInput = Input.GetAxis("Horizontal");
        float yawAmount = yawInput * yawSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yawAmount);

        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput = 1f;
        else if (Input.GetKey(KeyCode.E)) rollInput = -1f;

        float rollAmount = rollInput * rollSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rollAmount);
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustAmount = thrustSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * thrustAmount);

            if (engineAudio != null)
            {
                engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, thrustPitch, Time.deltaTime * 2f);
            }
        }
        else
        {
            if (engineAudio != null)
            {
                engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, idlePitch, Time.deltaTime * 2f);
            }
        }
    }

    private void ApplyCustomGravity()
    {
        transform.Translate(Vector3.down * gravityForce * Time.deltaTime, Space.World);
    }
}