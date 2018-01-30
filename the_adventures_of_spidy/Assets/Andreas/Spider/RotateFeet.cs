using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFeet : MonoBehaviour {

    //public float speed = 2f;
    public float maxRotation = 45f;
    public bool reverse = false;
    float _tempTime = 0;
    float _tempTimeReverse = 0;

    private Quaternion startingRotation;
    private float rot = 85;
    public float rotSpeed = 10;
    public float verticalOffset;

    public GameObject SpiderMainBody;



    // Use this for initialization
    void Start () {
        startingRotation = transform.rotation;


    }
	
	// Update is called once per frame
	void Update () {

        Rigidbody rb = SpiderMainBody.GetComponent<Rigidbody>();
        //Vector3 v3Velocity = rb.velocity;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        //if (v3Velocity.magnitude >= 0.3f) //Andreas: I will add this later, check velocity instead of keyboard input (above)
        {
            PingPongRotation();
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            //transform.rotation = startingRotation;
        }


        //transform.localEulerAngles = new Vector3(0, 0, -Mathf.PingPong(_tempTime * rotSpeed, rot));

    }

    void PingPongRotation()
    {
        _tempTime += Time.deltaTime * 10;
        //_tempTimeReverse += Time.deltaTime - Time.deltaTime;


        if (reverse == false)
        {
            transform.localEulerAngles = new Vector3(startingRotation.x, 0, verticalOffset + -Mathf.PingPong(_tempTime * rotSpeed, rot));
            //transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        }
        else if (reverse == true)
        {
            transform.localEulerAngles = new Vector3(startingRotation.x, 180, -(verticalOffset / 2 ) + Mathf.PingPong(_tempTime  * rotSpeed, rot));
            //transform.rotation = Quaternion.Euler(0f, 0f, -maxRotation * Mathf.Sin(Time.time * speed));
        }
    }


}
