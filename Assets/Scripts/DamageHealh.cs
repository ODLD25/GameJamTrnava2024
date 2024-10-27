using UnityEngine;

public class DamageHealh : MonoBehaviour
{
    [SerializeField]private float value;
    private bool damaged;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !damaged) {
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity += value; 
            damaged = true;
            Destroy(gameObject);
        }
    }
}
