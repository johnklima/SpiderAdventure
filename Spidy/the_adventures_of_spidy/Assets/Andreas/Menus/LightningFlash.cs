using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningFlash : MonoBehaviour {

    public float minTime = 0.5F;
    public float treshhold = 0.5F;
    public GameObject flash;

    private float lastTime = 0;

    public float Timer = 2f;

    private Image image;


    // Use this for initialization
    void Start () {
        image = flash.GetComponent<Image>();


    }

    void stopLightning()
    {
        flash.SetActive(false);
        Timer = 2F;
    }
	
	// Update is called once per frame
	void Update () {

        /*
        Timer -= Time.deltaTime;

        if (Timer <= 0f)
        {

            flash.SetActive(true);
            Debug.Log("FLASH");

            //Invoke("LaunchProjectile", 0.5F);
            Timer = 2F;

        }
        */

        if((Time.time - lastTime) > minTime)
        {
            if(Random.value * 1.5 > treshhold)
            {
                //flash.SetActive(true);
                image.enabled = true;
                Debug.Log("FLASH");
            }
            else
            {
                //flash.SetActive(false);
                image.enabled = false;
                lastTime = Time.time;
            }

        } 
    


    }
}
