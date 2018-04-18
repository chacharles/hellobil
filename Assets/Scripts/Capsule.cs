using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour {

    public static int nbCapsTot;
    int nbCaps;

	// Use this for initialization
	void Start () {
        nbCaps = nbCapsTot++;
	}
	
	// Update is called once per frame
	void Update () {

        transform.SetPositionAndRotation(
            new Vector3(
                Mathf.Sin(Time.time + 2.0f * Mathf.PI * ( (float) nbCaps / (float) Orbit.nbCapsMax) )*5.0f,
                Mathf.Cos(Time.time*6.0f + 8.0f * Mathf.PI * ((float) nbCaps / (float) Orbit.nbCapsMax) ),
                Mathf.Cos(Time.time + 2.0f * Mathf.PI * ((float) nbCaps / (float) Orbit.nbCapsMax) ) *6.0f
                ),
            Quaternion.Euler(
                new Vector3(
                    (Time.time * 6.0f + 2.0f * Mathf.PI * ((float)nbCaps / (float)Orbit.nbCapsMax)) * 5.0f,
                    (Time.time + 2.0f * Mathf.PI * ((float)nbCaps / (float)Orbit.nbCapsMax)) * 5.0f,
                    (Time.time * 10.0f + 2.0f * Mathf.PI * ((float)nbCaps / (float)Orbit.nbCapsMax)) * 5.0f
                    )
                )
            );		
	}
}
