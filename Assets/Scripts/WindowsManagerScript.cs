using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WindowsManagerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels = new List<GameObject>();

    private void Awake() {
        Time.timeScale = 1;
    }

    public void OpenLayout(string name)
    {
        foreach (var panel in panels)
        {
            if (panel.name == name) panel.SetActive(true);
            else panel.SetActive(false);
        }
    }

    public void StartGame(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
