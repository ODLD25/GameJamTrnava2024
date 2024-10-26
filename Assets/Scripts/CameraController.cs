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
    public GameObject soulPlayer;
    [SerializeField]private float soulTime;

    [Header("IDK")]
    [SerializeField]private GameObject background;
    [SerializeField]private float cameraMoveSpeed;
    private float soulTimer;
    [SerializeField]private bool soulCamera;
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
            Vector3 cameraPos = new Vector3(transform.position.x + (cameraMoveSpeed * Time.deltaTime), transform.position.y, transform.position.z);   
            transform.position = cameraPos;
        }
        else if (!soulCamera){
            Vector3 cameraPos = new Vector3(sidePlayer.transform.position.x, transform.position.y, transform.position.z);   
            transform.position = cameraPos;
        }

        Vector3 soulParentPos = new Vector3(sidePlayer.transform.position.x, soulParent.transform.position.y, soulParent.transform.position.z);   
        soulParent.transform.position = soulParentPos;

        Vector3 backgroundPos = new Vector3(sidePlayer.transform.position.x, background.transform.position.y, background.transform.position.z);
        background.transform.position = backgroundPos;
    }

    private void ChangeCamera(){
        if (soulCamera){
            Camera.main.transform.position = sideViewCameraPos;
            Camera.main.transform.rotation = sideViewCameraRot;

            sideParent.SetActive(true);
            soulParent.SetActive(false);

            soulCamera = false;
            soulTimer = soulTime;

            playerManager.GetComponent<PlayerManager>().soulView = soulCamera;
        }
        else {
            Camera.main.transform.position = new Vector3(sidePlayer.transform.position.x, soulCameraPos.y, soulCameraPos.z);
            Camera.main.transform.rotation = soulCameraRot;

            sideParent.SetActive(false);
            soulParent.SetActive(true);

            soulCamera = true;
            soulTimer = soulTime;
            
            playerManager.GetComponent<PlayerManager>().soulView = soulCamera;
        }
    } 
}
