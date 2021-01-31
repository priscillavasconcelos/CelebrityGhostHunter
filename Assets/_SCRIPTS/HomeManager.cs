using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public GameObject msgPanel;
    public GameObject instPanel;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenMsg()
    {
        msgPanel.SetActive(true);
    }

    public void OpenInstructions()
    {
        instPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instPanel.SetActive(false);
    }
}
