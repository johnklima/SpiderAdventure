using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraPlayer : MonoBehaviour {

    private const float Y_ANGLE_MIN = -25.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform LookAt;
    public Transform CamTransform;

    private Camera Cam;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;

    // Use this for initialization
    void Start () {

        CamTransform = transform;
        Cam = Camera.main;
		
	}

	// Update is called once per frame
	void Update () {

        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        //transform.position = Input.mousePosition; //Andreas:Test)

    }

    private void LateUpdate()
    {

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        CamTransform.position = LookAt.position + rotation * dir;
        CamTransform.LookAt(LookAt.position);
         
    }


	
}
