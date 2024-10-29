using UnityEngine;
using System.Collections.Generic;

public class ObstacleHolder : MonoBehaviour
{
    
    [Header("Obstacles")]
    [SerializeField]private List<GameObject> obstaclesInRange = new List<GameObject>();
    private GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        transform.position = player.transform.position;
    }

    public void DeleteObstacles(){
        foreach (GameObject obstacle in obstaclesInRange){
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity += 10;
            Debug.Log("Destroy");
            Destroy(obstacle);
        }

        obstaclesInRange.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Obstacle" && !obstaclesInRange.Contains(other.gameObject)){
            obstaclesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Obstacle" && obstaclesInRange.Contains(other.gameObject) && !Camera.main.GetComponent<CameraController>().soulCamera){
            obstaclesInRange.Remove(other.gameObject);
        }
    }
}
