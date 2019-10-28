using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject fallingObstaclePrefab;
    public GameObject linePrefab;
    public GameObject outPrefab;
	public GameObject treePrefab;
    public PlayerControl player;
    public float delayMin = 0.5f;
    public float delayMax = 5f;

    private float nextSpawnTime;
    private float treeTime;
    private float lastSlalomX;
	private Vector2 screenHalfSize;
    private float spawnSize;
    private float secondsBetweenSpawns;

    void Start()
    {
		screenHalfSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        lastSlalomX = 1f;
        spawnSize = 0.3f;
        secondsBetweenSpawns = Mathf.Lerp(delayMax, delayMin, SessionData.GetDifficultyPercent(SessionData.Counter));
        player = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        if (Time.time > treeTime)
        {
            treeTime = Time.time + secondsBetweenSpawns / 2;
            
            int randX = Random.Range((int)(-2 * (screenHalfSize.x)), (int)(2 * (screenHalfSize.x)));

            Vector2 treeSpawn1 = new Vector2(randX, -1 * screenHalfSize.y - 5);
            GameObject randomTree1 = (GameObject)Instantiate(treePrefab, treeSpawn1, Quaternion.identity);
            randomTree1.transform.localScale = Vector2.one * spawnSize * 1.5f;
        }

        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            
            float slalomx = lastSlalomX < 0 ? Random.Range(0, screenHalfSize.x / 4)
                    : Random.Range(-screenHalfSize.x / 4 - 5, 0);
            lastSlalomX = slalomx;

            float slalomy = -screenHalfSize.y - 15;
            Vector2 spawnPosition = new Vector2(slalomx, slalomy);
            Vector2 spawnPosition2 = new Vector2(slalomx + 5, slalomy);
            Vector2 spawnPositionLine = new Vector2(slalomx + (5 / 2) + 0.7f, slalomy);
            Vector2 spawnPositionLineLeft = new Vector2(-(screenHalfSize.x - slalomx) / 2.0F - 1, slalomy - spawnSize);
            Vector2 spawnPositionLineRight = new Vector2((slalomx + 5) + ((screenHalfSize.x - (slalomx + 5)) / 2) + 1, slalomy - spawnSize);

            GameObject slalomPostLeft = (GameObject)Instantiate(fallingObstaclePrefab, spawnPosition, Quaternion.identity);
            GameObject slalomPostRight = (GameObject)Instantiate(fallingObstaclePrefab, spawnPosition2, Quaternion.identity);
            
			GameObject pointLine = (GameObject)Instantiate(linePrefab, spawnPositionLine, Quaternion.identity);

            slalomPostLeft.transform.localScale = Vector2.one * spawnSize;
            slalomPostRight.transform.localScale = Vector2.one * spawnSize;
        }
	}
}
