using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool pause = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause) Resume();
            else Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pause = true;
    }

    public void OpenScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }
}
