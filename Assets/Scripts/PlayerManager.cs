using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Stats")]
    public float sanity;    

    [Header("Players")]
    [SerializeField]private GameObject sidePlayer;
    [SerializeField] private GameObject soulPlayer;

    [SerializeField]private GameObject health;
    [SerializeField]private GameObject gameOverMenu;
    private float maxSanity;

    public bool soulView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxSanity = sanity;
    }

    // Update is called once per frame
    void Update()
    {
        float width = Mathf.Clamp(sanity, 0f, 175f);
        health.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        if (sanity <= 0f){
            Die();
        }

        if (sanity > maxSanity){
            sanity = maxSanity;
        }
    }

    public void Die(){
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }
}
