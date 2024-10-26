using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Stats")]
    public float health;    

    [Header("Players")]
    [SerializeField]private GameObject sidePlayer;
    [SerializeField] private GameObject soulPlayer;
    public bool soulView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (soulView){
            
        }
    }
}
