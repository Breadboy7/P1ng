using UnityEngine;

public class BallSpeedColourChange : MonoBehaviour
{
    [Header("Color Settings")]
    public Color[] speedColors; // Assign colors in inspector
    public float minSpeed = 8f;
    public float maxSpeed = 25f;

    private Rigidbody rb;
    private Renderer ballRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (rb == null || ballRenderer == null || speedColors.Length == 0)
            return;

        float currentSpeed = rb.linearVelocity.magnitude;
        float speedPercent = Mathf.Clamp01((currentSpeed - minSpeed) / (maxSpeed - minSpeed));
        int colorIndex = Mathf.FloorToInt(speedPercent * (speedColors.Length - 1));

        ballRenderer.material.color = speedColors[colorIndex];
    }
}