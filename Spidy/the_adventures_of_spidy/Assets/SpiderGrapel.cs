using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TargetScript;

public class SpiderGrapel : MonoBehaviour {

    public RaycastHit hit;

    public LayerMask cullingmask;
    public int maxdistance;

    public bool IsInAir;
    public Vector3 loc;

    public float speed = 10;
    public Transform hand;

    public Camera cam;

    public LineRenderer LR;

    //Andreas: added this for target check
    public GameObject targetPointer;

    private GameObject currentTarget;
    public bool DestroyTargetOnEnd = false;
    public float destroyDelay = 0.3F;

    private AudioSource Source;
    public AudioClip TargetDestroy;
    public AudioClip ShootWeb;


    // Use this for initialization
    void Start ()
    {

        Source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckDistance();

        //if (Input.GetKey(KeyCode.Q)) //Andreas: Trying something here..
        if (Input.GetKeyDown(KeyCode.Q))
            Findspot();
        



        if (IsInAir)
            InAir();

        if(Input.GetKey(KeyCode.Space)&& IsInAir)
        {
            IsInAir = false;
            LR.enabled = false;
        }

    }

    public void Findspot()
    {

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxdistance, cullingmask))
        {

            /*IsInAir = true;
            loc = hit.point;
            LR.enabled = true;
            LR.SetPosition(1, loc);
            */
            //Andreas: Testing something here :) - use this for "tag only" - remember to disable lines above
            if (hit.collider.tag == "Targets") 
            {
                
                Debug.Log("Target is a coin!");
                IsInAir = true;
                loc = hit.point;
                LR.enabled = true;
                LR.SetPosition(1, loc);
                //Destroy(hit.transform.gameObject);
                currentTarget = hit.transform.gameObject;
                //DestroyTargetAfterDelay(currentTarget);
                Source.PlayOneShot(ShootWeb, 1f);
            }

        }
        
    }

    public void CheckDistance()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxdistance, cullingmask))
        {

            if (hit.collider.tag == "Targets")
            {
                targetPointer.SetActive(true);
            }
            else
            {
                targetPointer.SetActive(false);

            }

        }
        else
        {
            targetPointer.SetActive(false);
        }

    }

    public void InAir()
    {
        transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
        LR.SetPosition(0, hand.position);

        if(Vector3.Distance(transform.position, loc) < 0.5f)
        {
            IsInAir = false;
            LR.enabled = false;
            if (DestroyTargetOnEnd)
            {
                currentTarget.GetComponent<TargetScript>().HitTarget();
                //Destroy(currentTarget);
                Source.PlayOneShot(TargetDestroy, 1f);
            }
        }
    }


    void DestroyTargetAfterDelay(GameObject destroythis)
    {
        Destroy(destroythis, destroyDelay);
    }


}

