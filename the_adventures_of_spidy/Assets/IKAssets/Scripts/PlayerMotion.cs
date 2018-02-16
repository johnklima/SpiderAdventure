using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMotion : MonoBehaviour {

    //gravity in meters per second per second
    static float GRAVITY_CONSTANT = -9.8f;                      // -- for earth,  -1.6 for moon 
    static float MAX_WIND_CONSTANT = 10.0f;                      


    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity

    public Vector3 moveForce = new Vector3(0, 0, 0);            //combined force of all axis from input for move
    public Vector3 totalForce = new Vector3(0, 0, 0);           //total of ALL forces applied

    private Vector3 gravityNull = new Vector3(1, 0, 1);
    
    //character animation scripts triggered by state machine
    public AnimationScript walk;
    public AnimationScript idle;
    public AnimationScript jump;
    
    static int GROUND_LAYER = 1 << 8;

    //player characteristics
    public float mass = 1.0f;
    public float energy = 1.0f;
    public float verticalForce = GRAVITY_CONSTANT * -2;  //our jump force (Y axis)
    public float lateralForce = 10.0f;                   //our position force (X,Z axis)
    public float consumption = 0.0f;                     //energy burn rate
    public float friction = 0.975f;                      //TODO: put into world property
    public float lookRate = 3.0f;                        //interpolation rate for looking
    public float groundOffset = 1.0f;                    //how high off ground (terrain)
    public float groundRate = 1.0f;

    //surface/walkable handling
    public bool isJumping = false;
    public bool isOnPlatform = false;
    public bool isOnSurface = true;
    public float zmove = 0;
    public float xmove = 0;
    public float terrainHeight = 0;
    public Vector3 lastGoodPosition = new Vector3(0, 0, 0);
    
    //HILL handling 
    public Vector3 hillForceDir;
    public float hillAngle;
    public float hillFactor = 30;
    public Vector3 hillPolyNorm;

    //wind
    public Vector3 windForce = new Vector3(0, 0, 20);
    private float windTimer = 0;

    //Andreas
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float killUnderY = -35F;
    private int score;
    public Text scoreText;

    private AudioSource Source;
    public AudioClip CoinSound;

    public GameObject MainCam;

    // Use this for initialization
    void Start ()
    {
        score = 0;
        SetScoreText();

        Source = GetComponent<AudioSource>();

        windTimer = 0;

    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame

    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); //Andreas: I added this for debug purposes 
            Cursor.visible = true;
        }

        energy = 1;
        //make sure we are within the defined bounds of our level
        //isOutOfBounds returns True if we are out of bounds
        if (isOutOfBounds(isOnSurface) == false)
        {
            //all good so buffer last good position
            //lastGoodPosition = transform.position;

            //all good so do key input and movement
            handleInput();
            handleMovement();
        }

        //we always deal with terrain and player facing
        isOnSurface = handleTerrain();
        handleFacing();

        //TODO: dumb ass place to modulate wind - put this in the tree component
        //TODO: this equation can be almost anything that produces a smooth value 0-1, try Perlin!!
        windTimer += Time.deltaTime * Mathf.Abs(Mathf.Sin(Time.time));

        float wf = Mathf.Sin(windTimer);
        windForce.Set(0, 0.0f, wf);
        windForce *= MAX_WIND_CONSTANT;
         

    }
    private void LateUpdate()
    {
      
    }

    void handleInput()
    {

        float inputSpeed = 2600.0f; //30

        //clear out the move force each frame
        moveForce *= 0;
        Debug.Log("handle movement");


        //### ANDREAS - CUSTOM MOVEMENT ###//

        //Move Forward via Input, also checks for energy (might not use energy)
        //Andreas
        if (Input.GetKey(KeyCode.W) && energy > 0.0f)
        {
            //this was the original code by John
            //moveForce.z = lateralForce;

            //Getting transform.forward vector from MainCamera
            Vector3 forwardVector = MainCam.transform.forward;

            //Limiting directions - Should probably not use "new", but it works!
            Vector3 forwardVectorFinal = new Vector3(forwardVector.x, 0, forwardVector.z);

            //Taking forwardVector and multiplying with my local float variable (todo: make public)
            moveForce = forwardVectorFinal * inputSpeed * Time.deltaTime;

            // - I still keep this, might become useful
            energy -= consumption * Time.deltaTime;
        }

        //Move Back via Input, also checks for energy (might not use energy)
        //Andreas
        if (Input.GetKey(KeyCode.S) && energy > 0.0f)
        {
            //this was the original code by John
            //moveForce.z = -lateralForce;

            //Getting transform.forward vector from MainCamera - subtracting to get "back"
            Vector3 backVector = -MainCam.transform.forward;

            //Limiting directions - Should probably not use "new", but it works!
            Vector3 backVectorFinal = new Vector3(backVector.x, 0, backVector.z);

            //Taking forwardVector and multiplying with my local float variable (todo: make public)
            moveForce = backVectorFinal * inputSpeed * Time.deltaTime;

            // - I still keep this, might become useful
            energy -= consumption * Time.deltaTime;
        }


        //Move Left via Input, also checks for energy (might not use energy)
        //Andreas
        if (Input.GetKey(KeyCode.A) && energy > 0.0f)
        {
            //this was the original code by John
            //moveForce.x = -lateralForce;

            //Getting transform.right vector from MainCamera, subtracting to get "left"
            Vector3 leftVector = -MainCam.transform.right;

            //Limiting directions - Should probably not use "new", but it works!
            Vector3 leftVectorFinal = new Vector3(leftVector.x, 0, leftVector.z); 
            
            //Taking leftVector and multiplying with my local float variable (todo: make public)
            moveForce = leftVectorFinal * inputSpeed * Time.deltaTime;

            // - I still keep this, might become useful
            energy -= consumption * Time.deltaTime;
        }


        //Move Right via Input, also checks for energy (might not use energy)
        //Andreas
        if (Input.GetKey(KeyCode.D) && energy > 0.0f)
        {
            //this was the original code by John
            //moveForce.x = lateralForce;

            //Getting transform.right vector from MainCamera
            Vector3 rightVector = MainCam.transform.right;

            //Limiting directions - Should probably not use "new", but it works!
            Vector3 rightVectorFinal = new Vector3(rightVector.x, 0, rightVector.z);

            //Taking leftVector and multiplying with my local float variable (todo: make public)
            moveForce = rightVectorFinal * inputSpeed * Time.deltaTime;

            // - I still keep this, might become useful
            energy -= consumption * Time.deltaTime;
        }



        /*if (Input.GetKey(KeyCode.D) && energy > 0.0f) //RIGHT
        {
            //moveForce.x = lateralForce;

            Vector3 rightVector = MainCam.transform.right;
            moveForce = rightVector * inputSpeed;
            energy -= consumption * Time.deltaTime;

        }*/
        
    }

    void handleMovement()
    {
        //initial force of gravity
        if(isJumping)                           //we want absolute control of jump
            totalForce.Set(0, 0.0f , 0);
        else
            totalForce.Set(0, 1.0f, 0);

        totalForce *= GRAVITY_CONSTANT;

        //add our character moveForce
        totalForce += moveForce;
        totalForce += hillForceDir;

        //compare wind direction to our movement direction
        //should this be required
        Vector3 mf = windForce.normalized;
        Vector3 v = velocity.normalized;
        Vector3 wf = windForce;
        float ang = Vector3.SignedAngle(mf, v, Vector3.right);
        if (ang < 0)
            wf = windForce * -1;

        //TODO: reenable later
        //totalForce += windForce;
        //totalForce.Set(Vector3.forward);
        
        
        acceleration = totalForce / mass;
        velocity += acceleration * Time.deltaTime;

        //move the player //Andreas: Diabled atm..
        //transform.position += velocity * Time.deltaTime;
        /*
        //Andreas:
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 15.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 15.0f;

        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, z);
        */
        float maxForward = 20;
  
        velocity = Vector3.ClampMagnitude(velocity, maxForward);


       transform.position += velocity * Time.deltaTime;


        //decay velocity
        float y = velocity.y;
        velocity *= friction;
        velocity.Set(velocity.x, y, velocity.z);

        
    }

    bool isOutOfBounds(bool isOnSurface)
    {
        
        //keep the player within bounds

        
        bool ret = false;

        if (!isOnSurface)
        {

            Debug.Log("not on surface???");

            //transform.position = lastGoodPosition;

            //TODO: angle of incidence == angle of refraction, assume perpendicular plane 
            //velocity *= -1;
           // ret = true;
           //Andreas - this fixed my issue :D
        }
        else
        {
            ret = false;
        }
        return ret;
        
    }

    void handleFacing()
    {

        //face the character in the direction of their velocity
        Vector3 face = velocity.normalized;
        //pointAt(transform.position + face * 2.0f);
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

    }

    void pointAt(Vector3 target)
    {
        Quaternion a = transform.rotation;          //save it
        transform.LookAt(target);                   //look at target
        Quaternion b = transform.rotation;          //get new rotation
        transform.rotation = a;                     //set it back
        
        //spherical interpolate
        float t = Time.deltaTime;
        Quaternion c = Quaternion.Slerp(a, b, t * lookRate);

        transform.rotation = c;

    }


    private static Vector3 rayoffset = new Vector3(0, 1, 0);
    bool handleTerrain()
    {


        //we want to raycast
        float h = 1;
        RaycastHit hit;
        int layerMask = GROUND_LAYER;

        Vector3 raycastPoint = transform.position;
        raycastPoint += rayoffset;

        isOnPlatform = false;

        if (Physics.Raycast(raycastPoint, -Vector3.up, out hit, 10, layerMask))
        {

            if (hit.collider.gameObject.CompareTag("Ground"))
            {

                h = hit.point.y;

                hillPolyNorm = hit.normal;
                hillForceDir = Vector3.Cross(hillPolyNorm, Vector3.right);
                hillAngle = Vector3.SignedAngle(hillPolyNorm, Vector3.up, Vector3.right);

                //the force the hill apply from 0-1 max
                float hillForce = (hillAngle / 90) * hillFactor;
                hillForceDir *= hillForce;


            }
           
            
        }
        else
        {
            Debug.Log("NOT ON SURFACE");
            return false;

        }

        terrainHeight = h + groundOffset;

        //Andreas .. debug this
        //ensure I am NEVER below the surface
        if (transform.position.y <= terrainHeight)
        {
            Vector3 pos = new Vector3(transform.position.x, terrainHeight, transform.position.z);
            transform.position = pos;
            velocity.Scale( gravityNull );

        }


        return true;

    }
      

    private void OnTriggerEnter(Collider other)
    {

        //Andreas   
        if (other.gameObject.CompareTag("PickupItem"))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject); - Not using this
            Source.PlayOneShot(CoinSound, 1f);
            score = score + 100;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().EndGame();


        }


        Debug.Log("MY    OnTriggerEnter " + other.name);
        //transform.position = lastGoodPosition; //Andreas: Removed this


        //if (other.gameObject.tag == "testwall") - 
        if (other.gameObject.CompareTag("testwall"))
        {

            //Vector3 bounceVector = new Vector3(velocity.x, velocity.y, velocity.z);
            velocity.x *= -2;
            velocity.y *= -0.2f;
            velocity.z *= -2;

            //if (other.gameObject.transform.position.y + other.gameObject.transform.localScale.y > transform.position.y) //Andreas: added this so it does not bounce on top
            //{
            //velocity *= -2;           //bounce 2 = default
            //}
            //TODO: improve collision handling        
            //velocity *= -2;           //bounce 2 = default
        }
    }


    void FixedUpdate()
    {
        /*if (rb.position.y < killUnderY) //Andreas: Fall through level
        {
            FindObjectOfType<GameManager>().EndGame();
        }*/
    }


}
