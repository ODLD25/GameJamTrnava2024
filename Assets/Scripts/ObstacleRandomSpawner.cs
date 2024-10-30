using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomSpawner : MonoBehaviour
{
    [SerializeField]private List<GameObject> obstacles = new List<GameObject>(); 

    [SerializeField, Range(0, 20)]private float minDistance;
    [SerializeField, Range(20, 100)]private float maxDistance;
    [SerializeField]private Transform startSpawnPos;
    [SerializeField]private GameObject parent;

    private Vector3 lastObstacle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(obstacles[Random.Range(0, obstacles.Count)], startSpawnPos.position, Quaternion.identity, parent.transform.parent);
        lastObstacle = startSpawnPos.transform.position;

        InvokeRepeating(nameof(SpawnObstacles), 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacles(){
        if (gameObject.activeSelf == false) return;
        int randomObsticle = Random.Range(0, obstacles.Count);

        
        if (randomObsticle == 0){
            float chance = Random.Range(0f, 1f);

            if (chance < 0.2f){
                SpawnObstacles();
            }
        }

        float randomDistance =  Random.Range(minDistance, maxDistance);
        Vector2 spawnPos = new Vector3(lastObstacle.x + randomDistance, lastObstacle.y);

        Instantiate(obstacles[randomObsticle], spawnPos, Quaternion.identity, parent.transform.parent);
        lastObstacle = spawnPos;
    }
}
