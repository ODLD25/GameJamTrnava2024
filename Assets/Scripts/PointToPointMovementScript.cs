using UnityEngine;

public class PointToPointMovementScript : MonoBehaviour
{
    [Header("Points")]
    [SerializeField]private Transform pointA;
    [SerializeField]private Transform pointB;

    [SerializeField]private Transform target;
    [SerializeField]private float maxSpeed;

    private void Awake() {
        target = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, maxSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.5f){
            if (target == pointA){
                target = pointB;
            }
            else{
                target = pointA;
            }
        }
    }
}
