using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool pause = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity > 0 || Input.GetKeyDown(KeyCode.C) && GameObject.Find("PlayerManager").GetComponent<PlayerManager>().sanity > 0)
        {
            if (pause) Resume();
            else Pause();
        }
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Camera.main.GetComponent<CameraController>().lockedCam = true;

        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pause = true;
    }

    public void OpenScene(int scene)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        pause = false;

        Camera.main.GetComponent<CameraController>().lockedCam = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
