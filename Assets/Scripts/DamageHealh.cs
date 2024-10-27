using UnityEngine;

public class DamageHealh : MonoBehaviour
{
    [SerializeField]private float value;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("-");
        if (other.gameObject.tag == "Player") {
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity += value; 
            Destroy(gameObject);
        }
    }
}
