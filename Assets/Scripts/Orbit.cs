using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public Transform capsPrefab;
    public static int nbCapsMax = 10;

	// Use this for initialization
	void Start () {
        for (int i = 0;i<nbCapsMax;i++)
        {
            Instantiate(capsPrefab, new Vector3(i,i,i), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
