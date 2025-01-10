using UnityEngine;

public class WaterDetection : MonoBehaviour
{
    [SerializeField] GameManager gameScript;
    public PauseMenuManager pauseMenuManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && pauseMenuManager.isPaused == false) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && gameScript.isFishing == false)
            {
                Debug.Log($"Hit Collider: {hit.collider.name}, Tag: {hit.collider.tag}");

                if (hit.collider.CompareTag("Water"))
                {
                    Debug.Log("water");
                    gameScript.isFishing = true;
                    gameScript.StartCasting();
                }
                else
                    Debug.Log("not water");
            }
            else if(hit.collider == null  && gameScript.isFishing == false)
            {
                print("not water");
            }
        }
    }
}