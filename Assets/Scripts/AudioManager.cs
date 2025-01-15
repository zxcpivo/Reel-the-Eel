using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] footSteps;
    private AudioSource source;
    private bool isWalking = false; 
    public float stepInterval = 0.5f;

    public static AudioManager Instance; // Singleton instance


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();

        if (SettingsManager.Instance == null)
        {
            SettingsManager.Instance = FindObjectOfType<SettingsManager>();
        }


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
        while (isWalking && SettingsManager.Instance.GetSound())
        {
            AudioClip clip = footSteps[Random.Range(0, footSteps.Length)];
            source.PlayOneShot(clip);
            yield return new WaitForSeconds(stepInterval);
        }
    }
}

