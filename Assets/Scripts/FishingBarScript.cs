using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBarScript : MonoBehaviour
{
    public Rigidbody rb;
    public bool atTop;
    private float rodStrength = 1.4f;
    public float targetTime = 4.0f;
    public float savedTargetTime;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public GameObject p7;
    public GameObject p8;

    public bool onFish;
    public GameManager gameScript;


    void Update()
    {
        transform.localScale = new Vector3(0.4f, rodStrength, 0);

        if(onFish == true)
        {
            targetTime += Time.deltaTime;
        }
        if (onFish == false)
        {
            targetTime -= Time.deltaTime;
        }

        if(targetTime <= 0.0f) // losing game
        {
            transform.localPosition = new Vector3(-0.5f, -1.7f, 0);
            onFish = false;
            targetTime = 4.0f;
            gameScript.FishingMinigameLost();
        }

        if (targetTime >= 8.0f) // winning game
        {
            transform.localPosition = new Vector3(-0.5f, -1.7f, 0);
            onFish = false;
            targetTime = 4.0f;
            gameScript.FishingMinigameWon();
        }

        if(targetTime >= 0.0f)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 1.0f)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 2.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 3.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 4.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 5.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 6.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 7.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(true);
            p8.SetActive(false);
        }
        if (targetTime >= 8.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(true);
            p8.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            onFish = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            onFish = false;
        }
    }

    public void ChangeRodStrength(float strength)
    {
        rodStrength = strength;
    }
}
