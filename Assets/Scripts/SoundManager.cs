using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    AudioSource audioSource;
    public static SoundManager Instance;
    public List<SoundPair> sounds;
    Dictionary<string, AudioClip> soundDicitionary = new Dictionary<string, AudioClip>();


    // Start is called before the first frame update
     private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (SoundPair pair in sounds)
        {
            soundDicitionary.Add(pair.SoundName, pair.clip);
        }
    }


    public void PlaySound(string soundName, float volume)
    {
        if (soundDicitionary.TryGetValue(soundName, out AudioClip audioClip))
        {
            audioSource.PlayOneShot(audioClip, volume);
        }
        else
        {
            Debug.LogError($"could not find sound with the name {soundName}");
        }
    }

    [System.Serializable]
    public class SoundPair
    {
        public string SoundName;
        public AudioClip clip;
    }
}
