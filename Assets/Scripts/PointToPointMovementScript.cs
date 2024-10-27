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
    [SerializeField]private bool customLookAt;

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

        if (!goTillTheStart && !customLookAt) transform.LookAt(target);

        if (customLookAt && target == pointA){
            //Vector3 lookAtPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            Vector3 relativePos = target.position - transform.position;
            Quaternion lookAtRot = Quaternion.LookRotation(relativePos, Vector3.up);
            lookAtRot = Quaternion.Euler(lookAtRot.x, lookAtRot.y, lookAtRot.z);

            transform.rotation = lookAtRot;
        }
        else if (customLookAt && target == pointB){
            //Vector3 lookAtPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            Vector3 relativePos = target.position - transform.position;
            Quaternion lookAtRot = Quaternion.LookRotation(relativePos, Vector3.up);
            lookAtRot = new Quaternion(lookAtRot.x, lookAtRot.y - 90, lookAtRot.z, 0);

            transform.rotation = lookAtRot;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player"){
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity -= damage;
        }
    }
}
