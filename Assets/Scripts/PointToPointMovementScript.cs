using UnityEngine;

public class PointToPointMovementScript : MonoBehaviour
{
    [Header("Points")]
    [SerializeField]private Transform pointA;
    [SerializeField]private Transform pointB;

    [SerializeField]private Transform target;
    [SerializeField]private float maxSpeed;

    [SerializeField]private bool dealSpeedDamage;

    [SerializeField]private Vector3 pos, velocity;

    private void Awake() {
        target = pointA;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, maxSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.5f){
            if (target == pointA){
                target = pointB;
            }
            else{
                target = pointA;
            }
        }

        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;

        transform.LookAt(target);
    }

    private void OnCollisionEnter(Collision other) {
        /*if (other.gameObject.tag == "Player"){
            float damage = 
        }*/
    }
}
