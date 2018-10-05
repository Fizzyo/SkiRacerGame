using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject fallingObstaclePrefab;
    public GameObject linePrefab;
    public GameObject outPrefab;
    public Vector2 spawnDelayMinMax;
	float nextSpawnTime;
    float lastSlalomX;
	Vector2 screenHalfSize;

	void Start () {
		screenHalfSize = new Vector2 (Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        lastSlalomX = 1;
	}

	void Update () {

        if (Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(spawnDelayMinMax.y, spawnDelayMinMax.x, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            //			float spawnAngle = Random.Range (-spawnAngleMax, spawnAngleMax);
            float spawnSize = 0.7f;
            float slalomx = 5;
            if (lastSlalomX < 0)
            {
                slalomx = Random.Range(0, screenHalfSize.x / 4);
            }
            else
            {
                slalomx = Random.Range(-screenHalfSize.x / 4 - 5, 0);
            }
            /*while (Mathf.Abs(slalomx - lastSlalomX) > 5)
            {
                print("repositioning\n");
                slalomx = Random.Range(-screenHalfSize.x / 4 - 5, screenHalfSize.x / 4);
            }*/
            lastSlalomX = slalomx;
            float slalomy = -screenHalfSize.y - 15;
            Vector2 spawnPosition = new Vector2(slalomx, slalomy);
            Vector2 spawnPosition2 = new Vector2(slalomx + 5, slalomy);
            Vector2 spawnPositionLine = new Vector2(slalomx + (5 / 2) + 0.7f, slalomy);
            Vector2 spawnPositionLineLeft = new Vector2(-(screenHalfSize.x - slalomx) / 2.0F, slalomy);
            Vector2 spawnPositionLineRight = new Vector2((screenHalfSize.x - (slalomx + 5))/2.0F, slalomy);
            GameObject slalomPostLeft = (GameObject)Instantiate(fallingObstaclePrefab, spawnPosition, Quaternion.identity);
            GameObject slalomPostRight = (GameObject)Instantiate(fallingObstaclePrefab, spawnPosition2, Quaternion.identity);
            GameObject pointLine = (GameObject)Instantiate(linePrefab, spawnPositionLine, Quaternion.identity);

            GameObject pointLineLeft = (GameObject)Instantiate(outPrefab, spawnPositionLineLeft, Quaternion.identity);
             pointLineLeft.transform.localScale = new Vector3(screenHalfSize.x + slalomx, 0.1F, 0);
            

            GameObject pointLineRight = (GameObject)Instantiate(outPrefab, spawnPositionLineRight, Quaternion.identity);
            pointLineRight.transform.localScale = new Vector3(screenHalfSize.x - (slalomx + 5), 0.1F, 0);

            slalomPostLeft.transform.localScale = Vector2.one * spawnSize;
            slalomPostRight.transform.localScale = Vector2.one * spawnSize;
        }
	}
		
}
