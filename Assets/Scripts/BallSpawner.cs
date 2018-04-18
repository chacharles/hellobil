using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public Transform ballPrefab;
    public Transform spawnLocation;

    private static int numbBall = 0;
    //private GameObject[] ballList;
    private float shootRate = 2.0f;
    public static float countDown = 2.0f;

    private void Start()
    {
       // ballList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        if (countDown <= 0)
        {
            SpawnBall();
            countDown = shootRate;
         //   Debug.Log("Piooouuuuu");
        }
        countDown -= Time.deltaTime;

	}

    public static void DecreaseNumbBall()
    {
        numbBall--;
    }

    public void SpawnBall()
    {
        if (numbBall < 10)
        {
         //   Debug.Log("Spawn " + numbBall + " ");
            Instantiate(ballPrefab, spawnLocation.position, spawnLocation.rotation);
            numbBall++;
        }
    }
}
