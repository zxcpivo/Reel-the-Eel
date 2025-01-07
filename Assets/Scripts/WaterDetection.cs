using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDetection : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition); 

            TileBase clickedTile = tilemap.GetTile(gridPosition); 

            if (clickedTile != null) 
                Debug.Log("Water");

            else

                Debug.Log("Not water");

        }
    }

    private void Reel()
    {
        Debug.Log("Reel function called!");
        // Add your Reel functionality here
    }
}