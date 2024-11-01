using UnityEngine;

public class FlyingMovementScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float speed;
    [SerializeField]private float verticalSpeed;
    [SerializeField]private float maxYVelocity;

    [Header("Input")]
    [SerializeField]private KeyCode upKey = KeyCode.E;
    [SerializeField]private KeyCode downKey = KeyCode.Q;
    private float horizontalInput;
    private float verticalInput;

    [Header("References")]
    [SerializeField]private Rigidbody rb;

    [SerializeField]private Transform orientation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        SpeedHandler();
        VelocityYHandler();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move(){
        Vector3 direction = orientation.forward * verticalInput + transform.right * horizontalInput;
        
        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
    }

    private void GetInput(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(upKey)){
            rb.AddForce(Vector3.up * verticalSpeed * Time.deltaTime, ForceMode.Force);
        }

        if (Input.GetKey(downKey)){
            rb.AddForce(Vector3.down * verticalSpeed * Time.deltaTime, ForceMode.Force);
        }
    }

    private void SpeedHandler(){
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude > speed){
            Vector3 fixedVelocity = flatVelocity.normalized * speed;

            rb.linearVelocity = new Vector3(fixedVelocity.x, rb.linearVelocity.y, fixedVelocity.z);
        }
    }

    private void VelocityYHandler(){
        if (rb.linearVelocity.y > maxYVelocity){
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxYVelocity, rb.linearVelocity.z);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Finish") {
            Camera.main.GetComponent<CameraController>().ChangeCamera();
            GameObject.Find("ObstacleHolder").GetComponent<ObstacleHolder>().DeleteObstacles();
        }
    }
}
