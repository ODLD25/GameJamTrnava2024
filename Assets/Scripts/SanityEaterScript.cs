using System.Linq;
using UnityEngine;

public class SanityEaterScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]private float speed;

    [Header("References")]
    [SerializeField]private GameObject player;
    private GameObject playerManager;

    private float currentTakenHealth;
    private float maxTakenHealth = 20;

    private void Awake() {
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Debug.Log(Mathf.Lerp(currentTakenHealth, maxTakenHealth, 50 * Time.deltaTime));
    }
}
