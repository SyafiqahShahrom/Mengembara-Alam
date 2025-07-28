using UnityEngine;

public class TapToChangeColor : MonoBehaviour
{
    private Renderer[] renderers;
    
    // Calm, low-saturation colors (soft tones)
    private Color[] colors = {
        new Color(0.9f, 0.7f, 0.7f),  // soft pink
        new Color(0.7f, 0.9f, 0.7f),  // soft green
        new Color(0.7f, 0.7f, 0.9f),  // soft blue
        new Color(1.0f, 1.0f, 0.8f),  // soft yellow
        new Color(0.8f, 1.0f, 1.0f),  // soft cyan
        new Color(0.95f, 0.8f, 1.0f), // soft purple
        Color.clear                   // This will signal reset to original
    };

    private int currentColorIndex = 0;
    private Color[] originalColors; // To store original colors per material

    void Start()
    {
        // Get all renderers including children
        renderers = GetComponentsInChildren<Renderer>();
        
        // Save original colors (for first material of each renderer)
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                originalColors[i] = renderers[i].material.color;
            }
        }
    }

    void OnMouseDown()
    {
        // Go to next color index
        currentColorIndex = (currentColorIndex + 1) % colors.Length;

        // If it's the reset color (Color.clear used as signal), revert to original
        if (colors[currentColorIndex] == Color.clear)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Material[] mats = renderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    if (mats[j].HasProperty("_Color"))
                    {
                        mats[j].color = originalColors[i];
                    }
                }
            }
            return;
        }

        // Otherwise, apply the soft new color
        foreach (Renderer rend in renderers)
        {
            Material[] mats = rend.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i].HasProperty("_Color"))
                {
                    mats[i].color = colors[currentColorIndex];
                }
            }
        }
    }
}
