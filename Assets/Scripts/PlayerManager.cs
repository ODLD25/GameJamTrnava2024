using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Stats")]
    public float sanity;    

    [Header("Players")]
    [SerializeField]private GameObject sidePlayer;
    [SerializeField] private GameObject soulPlayer;

    [SerializeField]private GameObject health;
    [SerializeField]private GameObject gameOverMenu;

    [SerializeField] private float score;
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameObject dontDestroyOnLoad;

    private float maxSanity;

    public bool soulView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxSanity = sanity;
        dontDestroyOnLoad = GameObject.Find("DontDestroyOnLoad");
    }

    // Update is called once per frame
    void Update()
    {
        float width = Mathf.Clamp(sanity, 0f, 175f);
        health.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        score += Time.deltaTime;
        scoreText.text = score.ToString();

        if (Time.frameCount % 5 == 0) dontDestroyOnLoad.GetComponent<DontDestroyOnLoadScript>().score = score;

        if (sanity <= 0f){
            Die();
        }

        if (sanity > maxSanity){
            sanity = maxSanity;
        }
    }

    public void Die(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dontDestroyOnLoad.GetComponent<DontDestroyOnLoadScript>().score = score;

        Camera.main.GetComponent<CameraController>().lockedCam = true;

        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }
}
