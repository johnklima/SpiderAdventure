using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTargetLegLeft    : MonoBehaviour {

    public float frequency = 2;
    public float amplitude = 1.5f;
    public float phase = 0;

    Vector3 deltaT = new Vector3();
    Vector3 curT = new Vector3();
    float localTime = 0;
    float _tempTime = 0;

    private float rot = 0.4f;
    public float rotSpeed = 0.4f;

    // Use this for initialization
    void Start ()
    {
        deltaT = transform.localPosition;
        localTime = 0;

        transform.eulerAngles = new Vector3(10, 90, 0);

    }
	
	// Update is called once per frame
	void Update () {

        //transform.localEulerAngles = new Vector3(startingRotation.x, 0, verticalOffset + -Mathf.PingPong(_tempTime * rotSpeed, rot));
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) { 
            _tempTime += Time.deltaTime * 10;

        //float z = 0;//Mathf.Sin( (localTime + phase) * frequency) * amplitude;
        float z = Mathf.Sin( (localTime + phase) * frequency) * amplitude;
        float y = Mathf.Sin((localTime + phase) * frequency) * amplitude;
        curT.Set(transform.localPosition.x, transform.localPosition.y * -Time.deltaTime, deltaT.z + z);
        //curT.Set(transform.localPosition.x, transform.localPosition.y * -Time.deltaTime, transform.localPosition.z);
        //curT.Set(transform.localPosition.x, transform.localPosition.y * -Time.deltaTime, -Mathf.PingPong(_tempTime * rotSpeed, rot));


        transform.localPosition = curT;

        transform.eulerAngles = new Vector3(10, 90, 0);

        //accumulate our own local time to this object
        localTime += Time.deltaTime;
        }
        else
        {
            //float z = Mathf.Sin((localTime + phase) * frequency) * amplitude;
            //float y = Mathf.Sin((localTime + phase) * frequency) * amplitude;
            curT.Set(0.0f, 0.0f, 0.0f);

            transform.localPosition = curT;

            transform.eulerAngles = new Vector3(10, 90, 0);
        }
    }
}
