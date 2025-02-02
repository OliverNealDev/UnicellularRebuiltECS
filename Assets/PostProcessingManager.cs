using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public Volume volume;
    public Camera mainCamera;
    public float colorCycleDuration = 60.0f; // Duration of the full color cycle in seconds

    private Bloom bloom;
    private float timer = 0f;

    void Start()
    {
        // Ensure the Volume component is assigned
        if (volume == null)
        {
            Debug.LogError("Volume component is not assigned.");
            enabled = false;
            return;
        }

        // Ensure the Main Camera is assigned
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            enabled = false;
            return;
        }

        // Get the Bloom effect from the Volume component
        if (!volume.profile.TryGet(out bloom))
        {
            Debug.LogError("Bloom effect is not present in the Volume profile.");
            enabled = false;
        }
    }

    void Update()
    {
        // Update the timer for color cycling
        timer += Time.deltaTime;
        if (timer > colorCycleDuration)
        {
            timer -= colorCycleDuration;
        }

        // Calculate the t value for the color cycle
        float t = timer / colorCycleDuration;

        // Calculate the RGB values with a minimum value of 0.9
        float r = 0.9f + 0.1f * Mathf.Sin(2 * Mathf.PI * t + 0);
        float g = 0.9f + 0.1f * Mathf.Sin(2 * Mathf.PI * t + 2 * Mathf.PI / 3);
        float b = 0.9f + 0.1f * Mathf.Sin(2 * Mathf.PI * t + 4 * Mathf.PI / 3);

        // Set the Bloom tint color
        bloom.tint.value = new Color(r, g, b);

        // Get the camera's orthographic size
        float orthoSize = mainCamera.orthographicSize;
        float intensity = orthoSize / 75f;

        if (intensity < 1)
        {
            intensity = 1;
        }

        // Set the Bloom intensity
        if (intensity < 1.5f)
        {
            bloom.intensity.value = intensity;
        }
        else if (intensity != 1.5f)
        {
            bloom.intensity.value = 1.5f;
        }

        // Log the final intensity value being set
        //Debug.Log($"Final Bloom Intensity: {bloom.intensity.value}");
    }
}
