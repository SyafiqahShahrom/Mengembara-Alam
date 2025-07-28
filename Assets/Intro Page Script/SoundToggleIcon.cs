using UnityEngine;
using UnityEngine.UI;

public class SoundToggleIcon : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    
    [Header("Audio Settings")]
    public AudioSource musicSource; // 🎵 AudioSource to control
    
    [Header("Debug Info")]
    public bool debugMode = true;
    
    private Image buttonImage;
    private bool isSoundOn = true;

    void Start()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        // Try to get Image component
        buttonImage = GetComponent<Image>();
        
        // If no Image component found, try to get it from Button
        if (buttonImage == null)
        {
            Button button = GetComponent<Button>();
            if (button != null)
            {
                buttonImage = button.image;
            }
        }
        
        // Still no Image? Try to find it in children
        if (buttonImage == null)
        {
            buttonImage = GetComponentInChildren<Image>();
        }
        
        // Final check for Image component
        if (buttonImage == null)
        {
            Debug.LogError($"[SoundToggleIcon] No Image component found on '{gameObject.name}' or its children! Please add an Image component.");
            enabled = false; // Disable this script to prevent further errors
            return;
        }
        
        // Check if sprites are assigned
        if (soundOnSprite == null)
        {
            Debug.LogError($"[SoundToggleIcon] soundOnSprite not assigned on '{gameObject.name}'!");
            enabled = false;
            return;
        }
        
        if (soundOffSprite == null)
        {
            Debug.LogError($"[SoundToggleIcon] soundOffSprite not assigned on '{gameObject.name}'!");
            enabled = false;
            return;
        }
        
        // Set initial sprite
        buttonImage.sprite = soundOnSprite;
        
        // Check music source
        if (musicSource == null)
        {
            Debug.LogWarning($"[SoundToggleIcon] Music Source not assigned on '{gameObject.name}'! Audio control will be disabled.");
        }
        else
        {
            // Make sure music is playing at start if sound is on
            if (isSoundOn && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        
        // Set initial audio state
        AudioListener.volume = isSoundOn ? 1f : 0f;
        
        if (debugMode)
        {
            Debug.Log($"[SoundToggleIcon] Successfully initialized on '{gameObject.name}'");
        }
    }

    public void ToggleSound()
    {
        // Safety check
        if (buttonImage == null)
        {
            Debug.LogError($"[SoundToggleIcon] buttonImage is null! Cannot toggle sound on '{gameObject.name}'");
            return;
        }
        
        // Toggle sound state
        isSoundOn = !isSoundOn;
        
        if (isSoundOn)
        {
            // Turn sound ON
            buttonImage.sprite = soundOnSprite;
            AudioListener.volume = 1f;
            
            if (musicSource != null)
            {
                if (!musicSource.isPlaying)
                {
                    musicSource.Play();
                }
                else
                {
                    musicSource.UnPause();
                }
            }
            
            if (debugMode)
            {
                Debug.Log($"[SoundToggleIcon] Sound turned ON on '{gameObject.name}'");
            }
        }
        else
        {
            // Turn sound OFF
            buttonImage.sprite = soundOffSprite;
            AudioListener.volume = 0f;
            
            if (musicSource != null)
            {
                musicSource.Pause();
            }
            
            if (debugMode)
            {
                Debug.Log($"[SoundToggleIcon] Sound turned OFF on '{gameObject.name}'");
            }
        }
    }
    
    // Public method to set sound state programmatically
    public void SetSoundState(bool soundOn)
    {
        if (isSoundOn != soundOn)
        {
            ToggleSound();
        }
    }
    
    // Public method to get current sound state
    public bool GetSoundState()
    {
        return isSoundOn;
    }
    
    // Method to manually assign music source if needed
    public void SetMusicSource(AudioSource audioSource)
    {
        musicSource = audioSource;
        if (debugMode)
        {
            Debug.Log($"[SoundToggleIcon] Music source manually assigned on '{gameObject.name}'");
        }
    }
}