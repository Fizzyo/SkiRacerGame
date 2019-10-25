using UnityEngine;
using UnityEngine.UI;
using Fizzyo;


public class PlayerControl : MonoBehaviour {

    public bool isConfig;
	public float speed = 7f;
    public int left;
    public int counter;
    public event System.Action OnGameOver;

    public GameObject StreakText;
    public GameObject OuchText;
    public Text pointsTxt;
    public Sprite leftGuy;
    public Sprite rightGuy;
    
    private bool prevSpace;
    private int score;
    private int toDoBreaths;
    private int breaths;
    private float screenHalfWidth;

    private SpriteRenderer spriteR;

    private bool space = false;

    void Start()
    {
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
        score = 0;
        breaths = 0;
        toDoBreaths = BreathsCount.BreathsPer;
//        FizzyoFramework.Instance.Device.SetCalibrationPressure(0.4f);
        counter = 1;
        left = -1;
		float halfPlayerWidth = transform.localScale.x / 2f;
		screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        pointsTxt.text = score.ToString();
	}

    int GetScore()
    {
        return score;
    }

    void OnBreathStarted(object sender)
    {
        Debug.Log("Breath started");
    }

    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        breaths++;
    }

    public void SetBreathsPer(int breathes)
    {
        toDoBreaths = breathes;
    }

    void Update()
    {
        //print("Breaths per: " + toDoBreaths + " Breaths: " + breaths);

        if (isConfig)
            left = 0;

        if (breaths >= toDoBreaths)
        {
            OnGameOver?.Invoke();
        }
        
        speed = 7f * Difficulty.GetDifficultyPercent(counter) + 3f;
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
        score += amount;
    }

    private void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Avalanche")
        {
            counter = 0;
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
            score += 1;
            counter += 1;
            if (counter >= 4)
            {
                Vector2 spawnPosition = new Vector2(this.transform.position.x + 1, this.transform.position.y - 1);
                GameObject streakTextBox = (GameObject)Instantiate(StreakText, spawnPosition, Quaternion.identity);
                TextMesh theText = streakTextBox.transform.GetComponent<TextMesh>();
                theText.text = "Streak!";
                counter = 1;
                pointsTxt.color = new Color(1f, .17f, 0.0f);
            }
            pointsTxt.text = score.ToString();
        }
        else if (triggerCollider.tag == "Out")
        {
            counter = 2;
            pointsTxt.color = new Color(0, 0, 0);
            pointsTxt.text = score.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            counter = 1;
            Vector2 spawnPosition = new Vector2(this.transform.position.x + 1, this.transform.position.y + 1);
            GameObject streakTextBox = (GameObject)Instantiate(StreakText, spawnPosition, Quaternion.identity);
            TextMesh theText = streakTextBox.transform.GetComponent<TextMesh>();
            theText.text = "ouch";
            pointsTxt.color = new Color(0, 0, 0);

        }
    }
}
		 

