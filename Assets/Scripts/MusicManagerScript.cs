using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField]private AudioSource chillAudioSource;
    [SerializeField]private AudioSource actionAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.GetComponent<CameraController>().soulCamera){
            chillAudioSource.enabled = true;
            actionAudioSource.enabled = false;
        }
        else{
            chillAudioSource.enabled = false;
            actionAudioSource.enabled = true;
        } 
    }
}
