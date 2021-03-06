using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacles : MonoBehaviour {

    public float speedMin = 2f;
    public float speedMax = 5f;

	private float speed;
    private float visibleHeight;
    private bool original;

	void Start()
    {
        visibleHeight = Camera.main.orthographicSize - transform.localScale.y;
        original = false;
        PlayerControl player = FindObjectOfType<PlayerControl>();
        speed = Mathf.Lerp(speedMax, speedMin, Difficulty.GetDifficultyPercent(player.counter));
	}

	void Update()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        speed = Mathf.Lerp(speedMin, speedMax, Difficulty.GetDifficultyPercent(player.counter));
        transform.Translate (Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > visibleHeight + 2 * transform.localScale.y && !original)
        {
            original = true;
        }
        else if (transform.position.y > visibleHeight + 2 * transform.localScale.y)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControl pc = collision.gameObject.GetComponent<PlayerControl>();
        }
    }
}
