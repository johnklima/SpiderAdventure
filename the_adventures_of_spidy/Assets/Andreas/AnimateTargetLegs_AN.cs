using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTargetLegs_AN : MonoBehaviour
{
    //Public variables
    public float frequency = 10;
    public float amplitude = 4f;
    public float reverse = 0;
    public float yForce = 1.0f; // Acts as gravity

    Vector3 deltaT = new Vector3();
    Vector3 curT = new Vector3();
    float localTime = 0;
    float _tempTime = 0;

    //Rotation Tests
    private float rot = 0.4f;
    public float rotSpeed = 0.4f;


    void Start()
    {
        deltaT = transform.localPosition;
        localTime = 0;
        transform.eulerAngles = new Vector3(0, 90, 0);

    }

    void Update()
    {
        //Check for movement
        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            controlSpiderFeet();
           
    }

    void controlSpiderFeet()
    {
        //transform.localEulerAngles = new Vector3(startingRotation.x, 0, verticalOffset + -Mathf.PingPong(_tempTime * rotSpeed, rot));

        _tempTime += Time.deltaTime * 10;

        //float z = 0;//Mathf.Sin( (localTime + phase) * frequency) * amplitude;
        float z = Mathf.Sin((localTime + reverse) * frequency) * amplitude;
        float y = Mathf.Sin((localTime + reverse) * frequency) * amplitude;
        curT.Set(transform.localPosition.x, transform.localPosition.y * yForce * -Time.deltaTime, deltaT.z + z);

        //TODO: add speed here

        //curT.Set(transform.localPosition.x, transform.localPosition.y * -Time.deltaTime, transform.localPosition.z);
        //curT.Set(transform.localPosition.x, transform.localPosition.y * -Time.deltaTime, -Mathf.PingPong(_tempTime * rotSpeed, rot));


        transform.localPosition = curT;

        transform.eulerAngles = new Vector3(0, 90, 0);

        //Add time
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            localTime += Time.deltaTime;
        else
            localTime = 0;
    }


}
