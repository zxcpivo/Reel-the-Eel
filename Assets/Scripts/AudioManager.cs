using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] FootSteps;
    private AudioSource _source;
    private bool _isWalking = false; 
    public float StepInterval = 0.5f;

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
        _source = GetComponent<AudioSource>();

        if (SettingsManager.Instance == null)
        {
            SettingsManager.Instance = FindObjectOfType<SettingsManager>();
        }


    }

    public void StartFootsteps()
    {
        if (!_isWalking)
        {
            _isWalking = true;
            StartCoroutine(PlayFootsteps());
        }
    }

    public void StopFootsteps()
    {
        _isWalking = false;
    }

    private IEnumerator PlayFootsteps()
    {
        while (_isWalking && SettingsManager.Instance.GetSound())
        {
            AudioClip clip = FootSteps[Random.Range(0, FootSteps.Length)];
            _source.PlayOneShot(clip);
            yield return new WaitForSeconds(StepInterval);
        }
    }
}

