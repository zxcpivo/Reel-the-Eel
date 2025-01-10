using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovementMinigame : MonoBehaviour
{
    public GameObject fish;
    public GameObject miniGame;
    public GameObject top;
    public GameObject bottom;
    public float fishMovementInterval = 1.7f;

    void Update()
    {
        if (miniGame.activeSelf)
        {
            fishMovementInterval -= Time.deltaTime;
        }

        if (fishMovementInterval <= 0f)
        {
            int randomNum = Random.Range(1, 3);
            Vector3 newPosition = fish.transform.position;
            Vector3 topPosition = top.transform.position;
            Vector3 bottomPosition = bottom.transform.position;

            if (randomNum == 1 && newPosition.y <= topPosition.y - 1.1f)
            {
                newPosition.y += 1; // Move up
            }
            else if (randomNum == 2 && newPosition.y >= bottomPosition.y + 1.1f)
            {
                newPosition.y -= 1; // Move down
            }

            fish.transform.position = newPosition;
            fishMovementInterval = 2.0f;
        }
    }
}
