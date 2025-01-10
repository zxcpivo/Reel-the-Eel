using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] footSteps; 
    private AudioSource source;   
    private bool isWalking = false; 
    public float stepInterval = 0.5f; 

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartFootsteps()
    {
        if (!isWalking)
        {
            isWalking = true;
            StartCoroutine(PlayFootsteps());
        }
    }

    public void StopFootsteps()
    {
        isWalking = false;
    }
    private IEnumerator PlayFootsteps()
    {
        while (isWalking)
        {
            AudioClip clip = footSteps[Random.Range(0, footSteps.Length)];
            source.PlayOneShot(clip); 
            yield return new WaitForSeconds(stepInterval);
        }
    }
}