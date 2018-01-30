using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {

    public bool DestroyOnHit = false;
    public bool DestroyAfterDuration = false;
    public bool DeactivateTemp = false;

    public float lifetimeDuration = 1.0F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitTarget()
    {
        Debug.Log("TARGET HIT");
        if (DestroyOnHit == true)
        {
            Destroy(this.gameObject);
        }else if (DestroyAfterDuration == true)
        {
            Invoke("DestroyTarget", lifetimeDuration);
        }else if (DeactivateTemp == true)
        {
            this.gameObject.SetActive(false);
            Invoke("Reactivate", lifetimeDuration);
        }

    }

    void DestroyTarget()
    {
        Destroy(this);
    }

    void Reactivate()
    {
        this.gameObject.SetActive(true);
    }
}
