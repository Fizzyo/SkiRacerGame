using UnityEngine;
using UnityEngine.UI;
using Fizzyo;


public class PlayerControl : MonoBehaviour {

    public bool isConfig;
	public float speed = 7f;
    public int left;
    public event System.Action OnGameOver;

    public GameObject StreakText;
    public GameObject OuchText;
    public Text pointsTxt;
    public Sprite leftGuy;
    public Sprite rightGuy;
    
    private bool prevSpace;

    private float screenHalfWidth;

    private SpriteRenderer spriteR;

    private bool space = false;

    void Start()
    {
        SessionData.Session.StartSession(true);
        SessionData.Session.SessionComplete += (s, e) => { OnGameOver?.Invoke(); };

        SessionData.Score = 0;
        SessionData.Counter = 1;
        left = -1;
		float halfPlayerWidth = transform.localScale.x / 2f;
		screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        pointsTxt.text = SessionData.Score.ToString();


	}

    void Update()
    {
        //print("Breaths per: " + toDoBreaths + " Breaths: " + breaths);
        SessionData.Session.Update();

        if (isConfig)
            left = 0;
        
        speed = 7f * SessionData.GetDifficultyPercent(SessionData.Counter) + 3f;
        space = FizzyoFramework.Instance.Device.ButtonDown();

        if (space != prevSpace && space != false)
            left *= -1;

        prevSpace = space;

        spriteR.sprite = left > 0 ? leftGuy : rightGuy;

        float velocity = left * speed;

		transform.Translate(Vector2.right * velocity * Time.deltaTime);

		if (transform.position.x < -screenHalfWidth)
			transform.position = new Vector2(-screenHalfWidth, transform.position.y);

		if (transform.position.x > screenHalfWidth)
			transform.position = new Vector2(screenHalfWidth, transform.position.y);
	}

    public void IncreaseScore(int amount)
    {
        SessionData.Score += amount;
    }

    private void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Avalanche")
        {
            SessionData.Counter = 0;
            left = 0;
            pointsTxt.color = new Color(0, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if(triggerCollider.tag == "Avalanche")
        {
            left = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if(triggerCollider.tag == "Finish")
        {
            IncreaseScore(1);
            SessionData.Counter += 1;
            if (SessionData.Counter >= 4)
            {
                Vector2 spawnPosition = new Vector2(this.transform.position.x + 1, this.transform.position.y - 1);
                GameObject streakTextBox = (GameObject)Instantiate(StreakText, spawnPosition, Quaternion.identity);
                TextMesh theText = streakTextBox.transform.GetComponent<TextMesh>();
                theText.text = "Streak!";
                SessionData.Counter = 1;
                pointsTxt.color = new Color(1f, .17f, 0.0f);
            }
            pointsTxt.text = SessionData.Score.ToString();
        }
        else if (triggerCollider.tag == "Out")
        {
            SessionData.Counter = 2;
            pointsTxt.color = new Color(0, 0, 0);
            pointsTxt.text = SessionData.Score.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            SessionData.Counter = 1;
            Vector2 spawnPosition = new Vector2(this.transform.position.x + 1, this.transform.position.y + 1);
            GameObject streakTextBox = (GameObject)Instantiate(StreakText, spawnPosition, Quaternion.identity);
            TextMesh theText = streakTextBox.transform.GetComponent<TextMesh>();
            theText.text = "ouch";
            pointsTxt.color = new Color(0, 0, 0);

        }
    }
}
		 

