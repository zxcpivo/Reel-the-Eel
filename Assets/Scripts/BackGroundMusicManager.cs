using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicManager : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource source;

    float timer = 0;
    int count = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer <= 0 && SettingsManager.Instance.GetMusic())
        {
            // start new track
            AudioClip clip = tracks[count % tracks.Length];
            source.PlayOneShot(clip);
            timer = clip.length;
            count += 1;
        }
        if (!SettingsManager.Instance.GetMusic())
        {
            source.volume = 0f;
        }

        timer -= Time.deltaTime;
    }

}
