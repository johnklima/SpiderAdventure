using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public float speed = 10f;


    void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
    }
}
