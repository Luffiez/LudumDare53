using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField]
    AudioClip Music;
    [SerializeField]
    float Volume;
    // Start is called before the first frame update

    private void Start()
    {
        SoundManager.Instance.PlayMusic(Music, Volume);
        Destroy(this);
    }
}
