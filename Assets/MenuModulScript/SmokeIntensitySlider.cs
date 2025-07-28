using UnityEngine;
using UnityEngine.UI;

public class SmokeIntensitySlider : MonoBehaviour
{
    [Header("References")]
    public Slider intensitySlider;
    public SmokeClearer smokeClearer;
    
    void Start()
    {
        // Delay initialization to avoid conflicts with Vuforia
        StartCoroutine(DelayedInitialization());
    }
    
    System.Collections.IEnumerator DelayedInitialization()
    {
        // Wait a frame to let Vuforia initialize first
        yield return null;
        
        // Make sure we have references
        if (intensitySlider == null)
            intensitySlider = GetComponent<Slider>();
            
        if (smokeClearer == null)
            smokeClearer = FindObjectOfType<SmokeClearer>();
        
        // Set initial slider value to match the smoke clearer's initial intensity
        if (intensitySlider != null && smokeClearer != null)
        {
            intensitySlider.value = smokeClearer.initialSmokeIntensity;
        }
        
        // Subscribe to slider changes
        if (intensitySlider != null)
        {
            intensitySlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }
    
    void OnSliderValueChanged(float value)
    {
        // Update smoke intensity when slider changes
        if (smokeClearer != null)
        {
            smokeClearer.SetSmokeIntensity(value);
        }
    }
    
    void OnDestroy()
    {
        // Clean up listener
        if (intensitySlider != null)
        {
            intensitySlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}