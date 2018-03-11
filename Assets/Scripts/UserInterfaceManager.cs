using Microsoft.Xbox.Services.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the User Interface game objects
/// </summary>
public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;
    
    public Text ScoreText;
    public GameObject MainScreen;
    public GameObject GameScreen;
    public Transform[] HealthIcons;
    public IntegerStat stat;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            HealthIcons = GameScreen.transform.Find("Health").GetComponentsInChildren<Transform>();
            MainScreen.transform.Find("PlayButton").GetComponent<Button>().Select();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AdjustScore(int amount)
    {
        ScoreText.text = "Score: " + amount;
        if (SignInManager.Instance.GetPlayer(1).IsSignedIn)
        {
            stat.Increment();
            Debug.Log(stat.Value);
        }
    }


    /// <summary>
    /// Removes a heart from the UI.
    /// </summary>
    public void DecreasePlayerHealth()
    {
        for (int i = 1; i < HealthIcons.Length; i++)
        {
            if (HealthIcons[i].gameObject.activeSelf)
            {
                Debug.Log("Player health decreased");
                HealthIcons[i].gameObject.SetActive(false);
                break;
            }
        }

    }

    /// <summary>
    /// Starts the game by enabling the Game Manager and 
    /// </summary>
    public void Start_OnClick()
    {
        GameManager.Instance.StartGame();
        ScoreText.text = "Score: 0";

        for (int i = 0; i < HealthIcons.Length; i++)
        {
            HealthIcons[i].gameObject.SetActive(true);
        }

        MainScreen.SetActive(false);
        GameScreen.SetActive(true);

        Cursor.visible = false;
    }

    /// <summary>
    /// Ends the gam by removing enemy AI and toggling the appropriate UI
    /// </summary>
    public void EndGame()
    {
        StatsManagerComponent.Instance.RequestFlushToService(SignInManager.Instance.GetPlayer(1), true);

        GameManager.Instance.gameObject.SetActive(false);
        MainScreen.SetActive(true);
        GameScreen.SetActive(false);

        Cursor.visible = true;
    }
}
