using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    private Quaternion localRotation; // 
    public float speed = 1.0f; // ajustable speed from Inspector in Unity editor

    // Use this for initialization
    void Start()
    {
        // copy the rotation of the object itself into a buffer
        localRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount == 1)
        {
            transform.Translate(
             -Input.touches[0].deltaPosition.x * .025f,
             -Input.touches[0].deltaPosition.y * .025f,
             0.0f);
        }

        // find speed based on delta
        //float curSpeed = Time.deltaTime * speed;
        // first update the current rotation angles with input from acceleration axis
        localRotation.y = Input.acceleration.x;// * curSpeed;
        localRotation.x = Input.acceleration.y;// * curSpeed;

        // then rotate this object accordingly to the new angle
        //transform.rotation = localRotation;
    }
}
