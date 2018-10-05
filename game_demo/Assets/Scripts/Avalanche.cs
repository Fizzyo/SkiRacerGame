using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avalanche : MonoBehaviour {



	void Start () {
		transform.localPosition = new Vector2 (0, Camera.main.orthographicSize);
		transform.localScale = new Vector2 (Camera.main.aspect * Camera.main.orthographicSize * 2, 0);
	}

	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3(0,0.0025F,0);
//		transform.Translate (0, -0.004F, 0);

		if(Input.GetKey("up")) {
			if (transform.localScale.y <= 0) {
				transform.localScale = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * 2, 0);
			} else {
				transform.localScale -= new Vector3 (0, 0.01F, 0);
			}
		}
			
	}

}


