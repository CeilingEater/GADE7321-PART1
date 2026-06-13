using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; } //SINGLETON dingleton

    [Header("Setup")]
    [SerializeField] private List<SoundData> soundPool;
    
    private SFXHashMap sfxMap;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        
        sfxMap = new SFXHashMap(32); //im just doing 32 for no reason
        PopulateMap();
    }

    private void PopulateMap()
    {
        foreach (SoundData sound in soundPool)
        {
            if (!string.IsNullOrEmpty(sound.soundName) && sound.audioClip != null)
            {
                sfxMap.Put(sound.soundName, sound.audioClip);
            }
        }
    }
    
    public void PlaySFX(string soundName)
    {
        AudioClip clip = sfxMap.Get(soundName);

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFXManager: Sound '{soundName}' not found in the custom hash map.");
        }
    }
}