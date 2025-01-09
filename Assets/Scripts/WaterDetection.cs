using UnityEngine;

public class WaterDetection : MonoBehaviour
{
    [SerializeField] GameManager gameScript;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
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
            else
                Debug.Log("No collider detected.");
        }
    }
}