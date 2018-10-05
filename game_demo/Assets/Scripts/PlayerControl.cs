using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {

	public float speed = 7;
	float screenHalfWidth;
    public Text Points;
    private int score;
    public int left;
    float prevSpace;
    int counter;
	public event System.Action OnGameOver;

    void Start () {
        score = 0;
        counter = 0;
        left = -1;
		float halfPlayerWidth = transform.localScale.x / 2f;
		screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
	}

    public int getScore(){
		return score;
    }

	void Update () {
        speed = 7 * Difficulty.GetDifficultyPercent() + 3; 
		float space = Input.GetAxisRaw ("Jump");
        if(space != prevSpace && space != 0)
        {
            left = left * -1;
           
        }
        prevSpace = space;
        float velocity = left * speed;
		transform.Translate (Vector2.right * velocity * Time.deltaTime);

		if (transform.position.x < -screenHalfWidth) {
			transform.position = new Vector2 (-screenHalfWidth, transform.position.y);
		}

		if (transform.position.x > screenHalfWidth) {
			transform.position = new Vector2 (screenHalfWidth, transform.position.y);
		}

	}

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
 
        if (triggerCollider.tag == "Avalanche")
        {	
			print ("Avalanche touched");

			if (OnGameOver != null) {
				OnGameOver ();
			}

//			Destroy(gameObject);

			//			if (speed <= 2) {
			//				speed += 0;
			//			} else {
			//				speed--;	
			//			}
        }

        if (triggerCollider.tag == "Finish")
        {
            score += 1;
            counter = counter + 1;
            if (counter > 2)
            {
                score = score + counter;
            }

             Points.text = score.ToString();
        }
        if(triggerCollider.tag == "Out")
        {
            counter = 0;
            Points.text = score.ToString();
        }
    }
		 
}
