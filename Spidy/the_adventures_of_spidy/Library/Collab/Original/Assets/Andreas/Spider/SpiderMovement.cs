using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpiderMovement : MonoBehaviour {

    // Use this for initialization

    public float speed;
    public float torque;
    public Text scoreText;
    //public GameManager gameManager;

    private int score;
    private Rigidbody rb;


    //test for rotation
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float killUnderY = -35F;

    private AudioSource Source;
    public AudioClip CoinSound;


    void Start () {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();

        Source = GetComponent<AudioSource>();    
    }
	
	// Update is called once per frame //Andreas: I don't use this atm.
	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 15.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 15.0f;

        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); //Andreas: I added this for debug purposes 
            Cursor.visible = true;
        }


        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        transform.position = this.transform.position ;

    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * speed);
        //rb.velocity = movement * speed;


        //float turn = Input.GetAxis("Horizontal");

        //rb.rotation = Quaternion.Euler(0.0f, rb.velocity.x * 0.5, 0.0f);
        //rb.AddTorque(transform.up * torque * turn);


        if (rb.position.y < killUnderY) //Andreas: Fall through level
        {
            FindObjectOfType<GameManager>().EndGame();
        }
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
        }else if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().EndGame();


        }

    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

}
