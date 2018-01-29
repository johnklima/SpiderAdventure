using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {

    public Text scoreText;
    private int score;


	// Use this for initialization
	void Start () {
        score = 0;
        SetScoreText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupItem"))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            score = score + 1;
            SetScoreText();
        }

    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }
}
