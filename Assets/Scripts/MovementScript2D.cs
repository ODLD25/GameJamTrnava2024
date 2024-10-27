using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript2D : MonoBehaviour
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
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private bool grounded;

    [Header("Sounds")]
    [SerializeField]private AudioClip jumpClip;
    [SerializeField]private AudioSource walkAudioSource;
    [SerializeField]private AudioSource jumpAudioSource;

    [Header("References")]
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float playerHeight;

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
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        grounded = Physics2D.Raycast(currentPos, Vector2.down, playerHeight * 0.5f + 0.3f, layerMask);

        GetComponent<Animator>().SetBool("isJumping", !grounded);

        StateHandler();
        GetInput();
        DragHandler();
        SpeedHandler();
        VelocityYHandler();

        if (horizontalInput != 0f || verticalInput != 0f){
            walkAudioSource.enabled = true;
        }
        else{
            walkAudioSource.enabled = false;
        }
    }

    private void FixedUpdate() {
        Move();
    }
    
    private void OnBecameInvisible() {
        if (!Camera.main.GetComponent<CameraController>().soulCamera){
            Debug.Log("Game Over");
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().Die();
        }
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
        Vector2 direction = transform.right * horizontalInput;
        
        if (grounded){
            rb.AddForce(direction.normalized * moveSpeed * 10f, ForceMode2D.Force);
        }
        else{
            rb.AddForce(direction.normalized * moveSpeed * 10f * airMultiplier, ForceMode2D.Force);
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
        Vector2 flatVelocity = new Vector2(rb.linearVelocity.x, 0);

        if (flatVelocity.magnitude > moveSpeed){
            Vector2 fixedVelocity = flatVelocity.normalized * moveSpeed;

            rb.linearVelocity = new Vector2(fixedVelocity.x, rb.linearVelocity.y);
        }

        Vector2 velocity = new Vector2(rb.linearVelocity.x, 0);
        GetComponent<Animator>().SetFloat("Speed", velocity.magnitude);
    }

    private void VelocityYHandler(){
        if (rb.linearVelocity.y > maxYVelocity){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxYVelocity);
        }
    }

    private void Jump(){
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpAudioSource.PlayOneShot(jumpClip);
    }

    private void GetInput(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0f){
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else{
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

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
            transform.localScale = new Vector2(transform.localScale.x, crouchYScale);
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(crouchKey)){
            transform.localScale = new Vector2(transform.localScale.x, startYScale);
        }
    }
}
