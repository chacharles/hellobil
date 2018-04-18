using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody rb;
    public int ballNumb;
    Vector3 dir;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Random.insideUnitSphere*7.0f;
        transform.SetPositionAndRotation(new Vector3(0, 2, 0), Quaternion.identity);
//        dir = new Vector3(0.0f, 1.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //  transform.Translate(dir * speed * Time.deltaTime, Space.World);
        if (transform.localPosition.y < -2) {
            BallSpawner.DecreaseNumbBall();
           // Debug.Log("ball " + ballNumb + " died");
            Destroy(gameObject);
            return;
        }
    }
}
