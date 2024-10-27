using UnityEngine;
using System.Collections.Generic;

public class ObstacleHolder : MonoBehaviour
{
    
    [Header("Obstacles")]
    [SerializeField]private List<GameObject> obstaclesInRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Obstacle" && !obstaclesInRange.Contains(other.gameObject)){
            obstaclesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Obstacle" && obstaclesInRange.Contains(other.gameObject)){
            obstaclesInRange.Remove(other.gameObject);
        }
    }
}
