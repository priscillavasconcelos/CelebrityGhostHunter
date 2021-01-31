using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private Transform _gameParent;
    public Transform GameParent { get { return _gameParent; } }

    private int ghostsInRoom = 0;
    private int ghostsFound = 0;

    public List<Sprite> batteryLevels = new List<Sprite>();
    int batteryLevel = 0;
    public Image battery;

    //[SerializeField] TMP_Text ghostsInRoomText;
    //[SerializeField] TMP_Text ghostsFoundText;
    public Image dotCenter;

    [SerializeField] SmokeScreenController smokeScreen;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    internal void AddGhostsInRoom()
    {
        ghostsInRoom++;
        //ghostsInRoomText.text = "Ghosts In the Room: " + ghostsInRoom.ToString();
    }

    public void CaptureGhost(bool isCelebrity)
    {
        ghostsFound++;
        ghostsInRoom--;

        if (isCelebrity)
        {
            SceneManager.LoadScene("GameWin");
        }
        else
        {
            batteryLevel++;
            if (batteryLevel >= batteryLevels.Count)
            {
                SceneManager.LoadScene("GameOver_Battery");
            }
            battery.sprite = batteryLevels[batteryLevel];
        }
        
        //ghostsFoundText.text = "Ghosts Found: " + ghostsFound.ToString();
    }

    public void DestroyGhost(bool isCelebrity)
    {
        ghostsFound++;
        ghostsInRoom--;

        if (isCelebrity)
        {
            SceneManager.LoadScene("GameOver_DestroyCeleb");
        }

        //ghostsFoundText.text = "Ghosts Found: " + ghostsFound.ToString();
    }

    public void AddSmoke()
    {
        if (smokeScreen.gameObject.activeSelf == false)
        {
            smokeScreen.gameObject.SetActive(true);
        }
        else
        {
            smokeScreen.AddSmoke();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver_Smoke");
    }
}
