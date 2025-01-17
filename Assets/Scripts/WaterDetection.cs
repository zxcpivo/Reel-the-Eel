using UnityEngine;

public class WaterDetection : MonoBehaviour
{
    [SerializeField] GameManager gameScript;
    public PauseMenuManager pauseMenuManager;
    [SerializeField] private AudioClip splashSound;
    private AudioSource audioSource;

    public Shop shopScript;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (SettingsManager.Instance == null)
        {
            SettingsManager.Instance = FindObjectOfType<SettingsManager>();
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && pauseMenuManager.isPaused == false) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && gameScript.isFishing == false && shopScript.isShopping == false)
            {

                if (hit.collider.CompareTag("Water"))
                {
                    gameScript.isFishing = true;
                    gameScript.StartCasting(mousePosition);
                    if(SettingsManager.Instance.GetSound())
                        audioSource.PlayOneShot(splashSound);
                    
                }
            }
            else if(hit.collider == null  && gameScript.isFishing == false)
            {

            }
        }
    }
}