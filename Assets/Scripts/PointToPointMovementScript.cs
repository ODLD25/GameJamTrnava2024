using UnityEngine;

public class PointToPointMovementScript : MonoBehaviour
{
    [Header("Points")]
    [SerializeField]private Transform pointA;
    [SerializeField]private Transform pointB;

    [SerializeField]private Transform target;

    [Header("Stats")]
    [SerializeField]private float maxSpeed;

    [SerializeField]private float damage;
    [SerializeField]private bool goTillTheStart;

    [SerializeField]private Vector3 pos, velocity;

    private void Awake() {
        if (goTillTheStart){
            pointA = GameObject.Find("TaxiTarget").transform;
        }

        target = pointA;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (goTillTheStart && transform.position.x <= pointA.transform.position.x){
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, maxSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.5f && !goTillTheStart){
            if (target == pointA){
                target = pointB;
            }
            else{
                target = pointA;
            }
        }

        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;

        if (!goTillTheStart) transform.LookAt(target);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player"){
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity -= damage;
        }
    }
}
