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

    public bool soulView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float width = Mathf.Clamp(sanity, 0f, 175f);
        health.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        if (sanity <= 0f){
            Die();
        }
    }

    public void Die(){
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }
}
