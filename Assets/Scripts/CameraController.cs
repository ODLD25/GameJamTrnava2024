using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Side View")]
    [SerializeField]private Vector3 sideViewCameraPos;
    [SerializeField]private Quaternion sideViewCameraRot;
    [SerializeField]private GameObject sideParent;

    [Header("Soul View")]
    [SerializeField]private Vector3 soulCameraPos;
    [SerializeField]private Quaternion soulCameraRot;
    [SerializeField]private GameObject soulParent;
    [SerializeField]private float soulTime;
    private float soulTimer;
    [SerializeField]private bool soulCamera;
    
    private GameObject playerManager;

    private void Awake() {
        soulTimer = soulTime;
        soulParent.SetActive(false);
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
    }

    private void ChangeCamera(){
        if (soulCamera){
            Camera.main.transform.position = sideViewCameraPos;
            sideParent.SetActive(true);
            soulParent.SetActive(false);
            Camera.main.transform.rotation = sideViewCameraRot;
            soulCamera = false;
            soulTimer = soulTime;
            playerManager.GetComponent<PlayerManager>().soulView = soulCamera;
        }
        else {
            Camera.main.transform.position = soulCameraPos;
            sideParent.SetActive(false);
            soulParent.SetActive(true);
            Camera.main.transform.rotation = soulCameraRot;
            soulCamera = true;
            soulTimer = soulTime;
            playerManager.GetComponent<PlayerManager>().soulView = soulCamera;
        }
    } 
}
