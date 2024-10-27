using Unity.VisualScripting;
using UnityEngine;

public class PhysicMovementScript : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField]private float walkSpeed;
    [SerializeField]private float sprintSpeed;

    [Tooltip("The smaller this number the less controll of the speed and the higher the number the higher controll of speed.")]
    [SerializeField, Range(0f, 5f)]private float airMultiplier;

    [Tooltip("Player drag. This will change the drag value of rigidbody component when player is grounded.")]
    [SerializeField, Range(0, 10)]private float groundDrag;

    [Tooltip("Max speed that the player can go on the y-axis so up/down")]
    [SerializeField]private float maxYVelocity;
    private bool sprinting;

    [Header("Crouching")]
    [SerializeField]private float crouchSpeed;

    [Tooltip("The scale of the player when crouching")]
    [SerializeField, Range(0.0001f, 2f)]private float crouchYScale;
    private float startYScale;

    [Header("Input")]
    [SerializeField]private KeyCode jumpKey;
    [SerializeField]private KeyCode sprintKey;
    [SerializeField]private KeyCode crouchKey;
    private float horizontalInput;
    private float verticalInput;

    [Header("Jump")]
    [SerializeField]private float jumpForce;
    public bool grounded;

    [Header("References")]
    [SerializeField]private Rigidbody rb;
    [SerializeField]private float playerHeight;

    [Tooltip("Empty gameobject that should be a child of the player. The player takes the movement direction from this gameobject to prevent unwanted movement.")]
    [SerializeField]private Transform orientation;
    [SerializeField]private GameObject sidePlayer;

    public MovementState state;

    public enum MovementState{
        walking,
        sprinting,
        crouching,
        air
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f);

        StateHandler();
        GetInput();
        DragHandler();
        SpeedHandler();
        VelocityYHandler();
    }

    private void FixedUpdate() {
        Move();
    }

    private void StateHandler(){
        if (Input.GetKey(crouchKey) && !sprinting){
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if ((horizontalInput != 0 || verticalInput != 0) && grounded && sprinting){
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded){
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if (!grounded){
            state = MovementState.air;
        }
    }

    private void Move(){
        Vector3 direction = orientation.forward * verticalInput + transform.right * horizontalInput;
        
        if (grounded){
            rb.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else{
            rb.AddForce(direction.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void DragHandler(){
        if (grounded){
            rb.linearDamping = groundDrag;
        }
        else{
            rb.linearDamping = 0;
        }
    }

    private void SpeedHandler(){
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed){
            Vector3 fixedVelocity = flatVelocity.normalized * moveSpeed;

            rb.linearVelocity = new Vector3(fixedVelocity.x, rb.linearVelocity.y, fixedVelocity.z);
        }
    }

    private void VelocityYHandler(){
        if (rb.linearVelocity.y > maxYVelocity){
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxYVelocity, rb.linearVelocity.z);
        }
    }

    

    private void Jump(){
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void GetInput(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(jumpKey)){
            if (grounded){
                Jump();
            }
        }

        if (Input.GetKeyDown(sprintKey)){
            sprinting = true;
        }

        if (Input.GetKeyUp(sprintKey)){
            sprinting = false;
        }

        if (Input.GetKeyDown(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Finish") {
            Camera.main.GetComponent<CameraController>().ChangeCamera();
            GameObject.Find("ObstacleHolder").GetComponent<ObstacleHolder>().DeleteObstacles();
        }
    }
}
