using System.Linq;
using UnityEngine;

public class SanityEaterScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]private float takenHealthPerFrame;

    [Header("References")]
    private GameObject playerManager;

    private void Awake() {
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        float takenHealth = takenHealthPerFrame * Time.deltaTime;

        playerManager.GetComponent<PlayerManager>().sanity -= takenHealth;
    }
}
