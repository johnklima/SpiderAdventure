using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SawScript : MonoBehaviour {

    private Vector3 _startPosition;
    public float speed = 10f;
    public bool canMove = false;
    public bool vertical = false;
    public bool reverse = false;
    public float moveSpeed = 10f;

    private float min = 2f;
    private float max = 3f;
    public float distance = 30.0f;


    // Use this for initialization
    void Start () {
        _startPosition = transform.position;

        min = transform.position.x;
        max = transform.position.x + distance;
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.Rotate(0, 0, speed * Time.deltaTime);

        if (canMove)
        {
            //transform.position = _startPosition + new Vector3(Mathf.Sin(Time.time * moveSpeed) * 2 , 0.0f, 0.0f); - old test code
            //transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z); - old test code

            if (vertical == false) // Horizontal
            {
                if (reverse == false)
                {
                    transform.position = new Vector3(Mathf.PingPong(Time.time * moveSpeed, max - min) + min, transform.position.y, transform.position.z);
                }
                else if (reverse == true)
                {
                    transform.position = new Vector3(-Mathf.PingPong(Time.time * moveSpeed, max - min) + min, transform.position.y, transform.position.z);
                }

            }else if (vertical == true) { //Vertical
                if (reverse == false)
                {
                    transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * moveSpeed, max - min) + min, transform.position.z);
                }
                else if (reverse == true)
                {
                    transform.position = new Vector3(transform.position.x, -Mathf.PingPong(Time.time * moveSpeed, max - min) + min, transform.position.z);
                }
            }
            

        }

    }
}
