using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCamClass : MonoBehaviour
{


    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public bool hideCursor;
    public GameObject player;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Vector3 offset;



    // Use this for initialization
    void Start()
    {

        offset = transform.position - player.transform.position;

        if (hideCursor == true)
        {
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        float ClampValXMin = 65.0F; // UP
        float ClampValXMax = 35.0F; // DOWN

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -ClampValXMin, ClampValXMax);


        transform.eulerAngles = new Vector3(pitch, yaw, 0.0F);
        //transform.eulerAngles = new Vector3((Mathf.Clamp(pitch, -ClampValXMin, ClampValXMax)), yaw, 0.0F);
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        
        //transform.position = player.transform.position + offset;
        //Should be ON!!
    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
    }

}
