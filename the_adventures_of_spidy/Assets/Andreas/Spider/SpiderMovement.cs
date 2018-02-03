using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpiderMovement : MonoBehaviour
{

    // Use this for initialization

    public float speed;
    public float torque;
    public Text scoreText;
    //public GameManager gameManager;

    private int score;
    private Rigidbody rb;

    public bool stopRight;
    public bool stopLeft;
    public bool stopTop;
    public bool stopDown;
    public float sideOffsets = 0.3f;


    //test for rotation
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float killUnderY = -35F;

    private AudioSource Source;
    public AudioClip CoinSound;
    public GameObject MainCam;

    //From PlayerMotion
    //gravity in meters per second per second
    static float GRAVITY_CONSTANT = -9.8f;                      // -- for earth,  -1.6 for moon 
    static float MAX_WIND_CONSTANT = 10.0f;


    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity
    public Vector3 moveForce = new Vector3(0, 0, 0);            //combined force of all axis from input for move
    public Vector3 totalForce = new Vector3(0, 0, 0);           //total of ALL forces applied
    private Vector3 gravityNull = new Vector3(1, 0, 1);
    public float mass = 1.0f;
    public float friction = 0.975f;                      //TODO: put into world property


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();

        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame //Andreas: I don't use this atm.
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 15.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 15.0f;

        transform.Translate(x, 0, z);

        float movementSpeed = 103f;



        //Handle movement - START
        totalForce *= GRAVITY_CONSTANT;

        acceleration = totalForce / mass;
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    
        //decay velocity
        float y = velocity.y;
        velocity *= friction;
        velocity.Set(velocity.x, y, velocity.z);
        //Handle movement - END


        //transform.position += new Vector3(x * movementSpeed * Time.deltaTime, 0, z * movementSpeed * Time.deltaTime);

        /*
        if (!stopRight) { 
            if (Input.GetKey(KeyCode.D)) //Right
            {
                transform.position -= Vector3.left * movementSpeed * Time.deltaTime;
            }
        }

        if (!stopLeft)
        {
            if (Input.GetKey(KeyCode.A)) //Left
            {
                transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        
        if (!stopTop) {
        if (Input.GetKey(KeyCode.W)) //Up 
        {
            transform.position += new Vector3(0, 0, movementSpeed * Time.deltaTime);
        }
        }

        if (!stopDown)
            if (Input.GetKey(KeyCode.S)) //Down
        {
            transform.position -= new Vector3(0, 0, movementSpeed * Time.deltaTime);
        }
       
        //transform.Rotate(0, x, 0);
        //transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); //Andreas: I added this for debug purposes 
            Cursor.visible = true;
        }*/

        HandleGround();


        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        transform.position = this.transform.position;


    }

    void HandleGround()
    {
        RaycastHit hit;

        float m_MaxRay = 1.1f;
        Ray m_Ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(m_Ray, m_MaxRay))
        //if (Physics.Raycast(transform.position, -transform.up, out hit, m_MaxRay, 4))
        //if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxdistance, cullingmask))
        {

            Debug.Log("IS ON GROUND");
        }
        else
        {
            Debug.Log("NOT ON GROUND");
            velocity *= -1;
            transform.position -= new Vector3(0, 20 * Time.deltaTime, 0);
        }


    }

    void FixedUpdate()
    {
        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right, out hit))
        {
            if (hit.collider.tag == "Boundary")
                stopRight = false;
            else
                stopRight = false;
        }
        else
        {
            stopRight = true;
        }
        */
        

        stopRight = Physics.Raycast(transform.position, Vector3.right, sideOffsets);
        stopLeft = Physics.Raycast(transform.position, Vector3.left, sideOffsets);
        
        stopTop = Physics.Raycast(transform.position, Vector3.forward, sideOffsets);
        stopDown = Physics.Raycast(transform.position, Vector3.back, sideOffsets);





        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * speed);
        //rb.velocity = movement * speed;


        //float turn = Input.GetAxis("Horizontal");

        //rb.rotation = Quaternion.Euler(0.0f, rb.velocity.x * 0.5, 0.0f);
        //rb.AddTorque(transform.up * torque * turn);


        //if (rb.position.y < killUnderY) //Andreas: Fall through level
        //{
        //    FindObjectOfType<GameManager>().EndGame();
        //}
    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupItem"))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            Source.PlayOneShot(CoinSound, 1f);
            score = score + 100;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().EndGame();


        }

        if (other.gameObject.tag == "testwall")
        {
            //TODO: improve collision handling        
            //velocity *= -2;           //bounce 2 = default

        }

    }


}
