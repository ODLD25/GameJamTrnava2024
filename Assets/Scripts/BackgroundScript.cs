using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField]private float moveMultiplier = 0.5f;
    private Vector3 lastCamPos;
    private float textureUnitSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = Camera.main.transform.position - lastCamPos;
        newPos.z = 0;   
        transform.position += newPos * moveMultiplier;
        lastCamPos = Camera.main.transform.position;

        if (Camera.main.transform.position.x - transform.position.x >= textureUnitSize){
            float offsetPos = (Camera.main.transform.position.x - transform.position.x) % textureUnitSize;
            transform.position = new Vector3(Camera.main.transform.position.x + offsetPos, transform.position.y);
        }   
    }
}
