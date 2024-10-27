using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Side View")]
    [SerializeField]private Vector3 sideViewCameraPos;
    [SerializeField]private Quaternion sideViewCameraRot;
    [SerializeField]private GameObject sideParent;
    [SerializeField]private GameObject sidePlayer;

    [Header("Soul View")]
    [SerializeField]private Vector3 soulCameraPos;
    [SerializeField]private Quaternion soulCameraRot;
    [SerializeField]private GameObject soulParent;
    [SerializeField]private GameObject soulPlayer;
    [SerializeField]private float soulTime;

    [Header("IDK")]
    [SerializeField]private GameObject background;
    [SerializeField]private GameObject road;
    [SerializeField]private float cameraMoveSpeed;
    private float soulTimer;
    public bool soulCamera;
    private GameObject playerManager;

    private void Awake() {
        soulTimer = soulTime;
        soulParent.SetActive(false);
        playerManager = GameObject.Find("PlayerManager");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (soulCamera) {soulTimer -= Time.deltaTime;}

        if (Input.GetKeyDown(KeyCode.Tab) || soulTimer <= 0f){
            ChangeCamera();
        }
        
        if (Camera.main.transform.position.x >= sidePlayer.transform.position.x && !soulCamera){
            Vector3 cameraPos = new Vector3(transform.position.x + (cameraMoveSpeed * Time.deltaTime), 0, transform.position.z);   
            transform.position = cameraPos;
        }
        else if (!soulCamera){
            Vector3 cameraPos = new Vector3(sidePlayer.transform.position.x, 0, transform.position.z);   
            transform.position = cameraPos;
        }

        Vector3 soulParentPos = new Vector3(sidePlayer.transform.position.x, soulParent.transform.position.y, soulParent.transform.position.z);   
        soulParent.transform.position = soulParentPos;

        Vector3 backgroundPos = new Vector3(sidePlayer.transform.position.x, background.transform.position.y, background.transform.position.z);
        background.transform.position = backgroundPos;

        Vector3 roadPos = new Vector3(sidePlayer.transform.position.x, road.transform.position.y, road.transform.position.z);
        road.transform.position = roadPos;
    }

    public void ChangeCamera(){
        if (soulCamera){
            soulCamera = false;

            Camera.main.transform.position = sideViewCameraPos;
            Camera.main.transform.rotation = sideViewCameraRot;

            sideParent.SetActive(true);
            soulParent.SetActive(false);

            soulTimer = soulTime;

            playerManager.GetComponent<PlayerManager>().soulView = soulCamera;
        }
        else{
            soulCamera = true;

            Camera.main.transform.position = new Vector3(sidePlayer.transform.position.x, soulCameraPos.y, soulCameraPos.z);
            Camera.main.transform.rotation = soulCameraRot;

            sideParent.SetActive(false);
            soulParent.SetActive(true);

            soulTimer = soulTime;
            
            playerManager.GetComponent<PlayerManager>().soulView = soulCamera;
        }
    } 
}
