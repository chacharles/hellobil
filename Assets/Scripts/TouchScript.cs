using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {

    public Camera camera;
    public AudioClip shootSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    { 
        // get one click
        if (Input.GetMouseButtonDown(0) == true)
        {
            // spawn a Ball
            Debug.Log(Input.GetMouseButtonDown(0).ToString());
            BallSpawner.countDown = 0.0f;
//        }

  //      if (Input.touchCount == 1)
  //      {
            // get object under touche
            Ray touchRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(touchRay);
            foreach (RaycastHit hit in hits)
            {
                Debug.Log("touching object name=" + hit.transform.gameObject.name);
                if (hit.transform.gameObject.name.Equals("Ball(Clone)"))
                {
                    Debug.Log("PAAAF");
                    audioSource.PlayOneShot(shootSound, 1.0f);
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    rb.velocity = Random.insideUnitSphere * 7.0f;
                }
            }
        }

        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * 0.1f;// orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * 0.1f;//perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }

    void OnGUI() {

        // get one touch 
    /*    if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).fingerId == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    // spawn a Ball
                    Debug.Log("Spawn Touch !");
                    BallSpawner.countDown = 0.0f; ;

                }
            }
        }*/

        // display touch info on screen
        foreach (Touch touch in Input.touches){
            string mess = "";
            mess += "Id " + touch.fingerId + "\n";
            mess += "Phase " + touch.phase.ToString() + "\n";
            mess += "TapCount " + touch.tapCount + "\n";
            mess += "Pos x " + touch.position.x + "\n";
            mess += "Pos y " + touch.position.y + "\n";

            int n = touch.fingerId;
            GUI.Label(new Rect(130*(n+2),0,120,100),mess);
        }
    }
}
