using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 3f;
    private GameObject playerManager;
    [SerializeField] private float damage = 0.1f;

    private void Awake()
    {
        playerManager = GameObject.Find("PlayerManager");
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            float takenHealth = damage * Time.deltaTime;

            playerManager.GetComponent<PlayerManager>().sanity -= takenHealth;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

   
}
