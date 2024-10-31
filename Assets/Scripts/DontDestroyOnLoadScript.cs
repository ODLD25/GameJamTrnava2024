using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    public float score;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
