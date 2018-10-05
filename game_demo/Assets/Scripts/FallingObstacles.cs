using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacles : MonoBehaviour {

	public Vector2 speedMinMax;
	float speed;
    float visibleHeight;

	void Start(){
        visibleHeight = Camera.main.orthographicSize - transform.localScale.y;
		speed = Mathf.Lerp (speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent ());
	}

	void Update () {
		transform.Translate (Vector3.up * speed * Time.deltaTime);
        if(transform.position.y > visibleHeight + 2 * transform.localScale.y)
        {
            Destroy(gameObject);
        }
	}
}
