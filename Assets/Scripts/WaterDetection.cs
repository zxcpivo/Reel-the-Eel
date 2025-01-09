using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDetection : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;

    public GameManager gameScript;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = _tilemap.WorldToCell(mousePosition); 

            TileBase clickedTile = _tilemap.GetTile(gridPosition); 

            if (clickedTile != null && gameScript.isFishing == false)
            {
                gameScript.isFishing = true;
                gameScript.StartCasting();
            }

            if(clickedTile == null && gameScript.isFishing == false)
            {
                Debug.Log("Not water");
            }

            //f
        }
    }
}   