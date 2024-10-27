using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomSpawner : MonoBehaviour
{
    [SerializeField]private List<GameObject> obstacles = new List<GameObject>(); 

    [SerializeField, Range(0, 20)]private float minDistance;
    [SerializeField, Range(20, 50)]private float maxDistance;
    [SerializeField]private Transform startSpawnPos;

    private GameObject lastObstacle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count - 1)], startSpawnPos.position, Quaternion.identity);

        InvokeRepeating(nameof(SpawnObstacles), 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacles(){
        float randomDistance =  Random.Range(minDistance, maxDistance);
        Vector2 spawnPos = new Vector3(lastObstacle.transform.position.x + randomDistance, lastObstacle.transform.position.y);

        lastObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count - 1)], spawnPos, Quaternion.identity);
    }
}
