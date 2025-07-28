using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for Event Systems
using System.Collections; // Required for Coroutines

public class SmokeClearer : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Image smokeImage; // Reference to the Image component of the smoke overlay
    public float clearRadius = 50f; // Radius of the area cleared by each wipe
    public float clearThreshold = 0.8f; // Percentage of smoke cleared to trigger completion
    public AudioSource swooshSound; // AudioSource for the swoosh sound
    public AudioClip swooshClip; // AudioClip for the swoosh sound
    public long vibrationDurationMs = 50; // Haptic feedback duration in milliseconds

    [Header("Smoke Intensity Settings")]
    [Range(0f, 1f)] // Makes the slider in the Inspector go from 0 to 1
    public float initialSmokeIntensity = 1.0f; // Default starting intensity (1.0 = fully opaque)

    private Texture2D smokeTexture;
    private Color[] initialPixels;
    private int clearedPixelCount = 0;
    private int totalOpaquePixels = 0;
    private bool miniGameCompleted = false;

    // Event for when the mini-game is completed
    public delegate void MiniGameCompleted();
    public static event MiniGameCompleted OnMiniGameCompleted;

    void Awake()
    {
        if (smokeImage == null)
        {
            smokeImage = GetComponent<Image>();
        }

        if (smokeImage != null && smokeImage.sprite != null)
        {
            // Get the texture from the sprite
            smokeTexture = smokeImage.sprite.texture;

            // Ensure the texture is readable
            if (!smokeTexture.isReadable)
            {
                Debug.LogError("Smoke Texture is not readable. Please set 'Read/Write Enabled' in the Texture Import Settings.");
                return;
            }

            // Create a copy to modify
            smokeTexture = DuplicateTexture(smokeTexture);
            smokeImage.sprite = Sprite.Create(smokeTexture, new Rect(0, 0, smokeTexture.width, smokeTexture.height), new Vector2(0.5f, 0.5f));

            initialPixels = smokeTexture.GetPixels();

            // Count total opaque pixels for completion check
            foreach (Color pixel in initialPixels)
            {
                if (pixel.a > 0.01f) // Consider pixels with some alpha as opaque
                {
                    totalOpaquePixels++;
                }
            }

            Debug.Log($"Total opaque pixels: {totalOpaquePixels}");

            // Apply the initial smoke intensity
            SetSmokeIntensity(initialSmokeIntensity);
        }
        else
        {
            Debug.LogError("Smoke Image or its Sprite is null. Please assign them in the Inspector.");
        }

        if (swooshSound == null)
        {
            swooshSound = gameObject.AddComponent<AudioSource>();
            swooshSound.playOnAwake = false;
        }
        if (swooshClip != null)
        {
            swooshSound.clip = swooshClip;
        }
    }

    // New public method to set smoke intensity from a slider
    public void SetSmokeIntensity(float intensity)
    {
        if (smokeImage != null)
        {
            Color currentColor = smokeImage.color;
            // Ensure alpha is clamped between 0 and 1
            smokeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Clamp01(intensity));
            Debug.Log($"Smoke Intensity set to: {intensity}");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (miniGameCompleted) return; // Prevent interaction after game is done
        ClearSmokeAtPosition(eventData.position);
        PlaySwooshAndVibrate();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (miniGameCompleted) return; // Prevent interaction after game is done
        ClearSmokeAtPosition(eventData.position);
        PlaySwooshAndVibrate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (miniGameCompleted) return; // Prevent interaction after game is done
        CheckCompletion();
    }

    void ClearSmokeAtPosition(Vector2 screenPosition)
    {
        if (miniGameCompleted || smokeTexture == null) return;

        // Convert screen position to local position on the RawImage
        RectTransformUtility.ScreenPointToLocalPointInRectangle(smokeImage.rectTransform, screenPosition, Camera.main, out Vector2 localPoint);

        // Convert local position to texture coordinates
        // localPoint is relative to the center of the rectTransform. Adjust to bottom-left origin.
        float normalizedX = (localPoint.x - smokeImage.rectTransform.rect.x) / smokeImage.rectTransform.rect.width;
        float normalizedY = (localPoint.y - smokeImage.rectTransform.rect.y) / smokeImage.rectTransform.rect.height;

        int pixelX = Mathf.FloorToInt(normalizedX * smokeTexture.width);
        int pixelY = Mathf.FloorToInt(normalizedY * smokeTexture.height);

        int radiusPixels = Mathf.RoundToInt(clearRadius / (smokeImage.rectTransform.rect.width / smokeTexture.width));

        for (int y = -radiusPixels; y <= radiusPixels; y++)
        {
            for (int x = -radiusPixels; x <= radiusPixels; x++)
            {
                if (x * x + y * y <= radiusPixels * radiusPixels) // Circular clearing
                {
                    int currentX = pixelX + x;
                    int currentY = pixelY + y;

                    if (currentX >= 0 && currentX < smokeTexture.width && currentY >= 0 && currentY < smokeTexture.height)
                    {
                        int index = currentY * smokeTexture.width + currentX;
                        Color originalPixelColor = initialPixels[index]; // Use the initial pixel color

                        if (originalPixelColor.a > 0.01f) // If it was an opaque pixel in the original texture
                        {
                            if (smokeTexture.GetPixel(currentX, currentY).a > 0.01f) // If not already cleared on the current texture
                            {
                                // Make it fully transparent, but retain original RGB for consistency if needed
                                smokeTexture.SetPixel(currentX, currentY, new Color(originalPixelColor.r, originalPixelColor.g, originalPixelColor.b, 0f));
                                clearedPixelCount++;
                            }
                        }
                    }
                }
            }
        }
        smokeTexture.Apply(); // Apply changes to the texture
    }

    void CheckCompletion()
    {
        if (miniGameCompleted) return;

        if (totalOpaquePixels == 0) // Avoid division by zero if no opaque pixels were found initially
        {
            Debug.LogWarning("No opaque pixels found in the smoke texture. Mini-game will complete immediately.");
            miniGameCompleted = true;
            OnMiniGameCompleted?.Invoke();
            return;
        }

        float clearedPercentage = (float)clearedPixelCount / totalOpaquePixels;
        Debug.Log($"Cleared: {clearedPixelCount}/{totalOpaquePixels} ({clearedPercentage * 100:F2}%)");

        if (clearedPercentage >= clearThreshold)
        {
            Debug.Log("Mini-game Completed!");
            miniGameCompleted = true;
            OnMiniGameCompleted?.Invoke();
            // You might want to animate the remaining smoke fading out or disable the overlay immediately
            StartCoroutine(FadeOutSmokeOverlay());
        }
    }

    IEnumerator FadeOutSmokeOverlay()
    {
        float duration = 1.0f; // Fade out over 1 second
        float timer = 0f;
        Color startColor = smokeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            smokeImage.color = Color.Lerp(startColor, targetColor, timer / duration);
            yield return null;
        }
        smokeImage.color = targetColor;
        smokeImage.gameObject.SetActive(false); // Hide the smoke overlay after fading
    }

    void PlaySwooshAndVibrate()
    {
        if (swooshSound != null && swooshClip != null && !swooshSound.isPlaying)
        {
            swooshSound.PlayOneShot(swooshClip);
        }

        // Haptic feedback
        #if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate(); // Basic vibration on mobile
        // For more advanced haptics, you might need third-party plugins or native code.
        #endif
    }

    // Helper to duplicate a texture and make it readable
    Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    void OnDestroy()
    {
        // Clean up event subscription
        OnMiniGameCompleted = null;
    }
}