using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

/// <summary>
/// Manages the basics of the game such as spawning and communicating with the user interface
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("General")]
    public UserInterfaceManager UserInterface;

    [Header("Spawning")]
    public GameObject Player;
    public List<GameObject> SpawnPoints = new List<GameObject>();

    private int _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void StartGame()
    {
        TogglePlayer(true);
        Player.GetComponent<Player>().CurrentHitPoints = 3;

        _score = 0;
        SpawnEnemy();
    }

    /// <summary>
    /// Spawns an enemy AI and recusively invokes itself to ensure spawning continues. Automatically stops running when the script has been disabled.
    /// </summary>
    public void SpawnEnemy()
    {
        int enemy = Random.Range(0, 2);
        int spawnLocation = Random.Range(0, SpawnPoints.Count);

        ObjectPoolManager.ObjectType enemySize = enemy == 1 ? ObjectPoolManager.ObjectType.EnemySmall : ObjectPoolManager.ObjectType.EnemyLarge;
        GameObject enemyGameObject = ObjectPoolManager.Instance.RequestObject(enemySize);
        enemyGameObject.SetActive(true);

        enemyGameObject.GetComponent<Enemy>().SetupEnemy(SpawnPoints[spawnLocation].transform.position);
        Invoke("SpawnEnemy", Random.Range(1, 5));
    }

    /// <summary>
    /// Toggles the player game object.
    /// </summary>
    /// <param name="visible"></param>

    public void TogglePlayer(bool visible)
    {
        Player.SetActive(visible);
    }

    /// <summary>
    /// Increments the score and calls the UI for display.
    /// </summary>
    public void IncrementScore()
    {
        _score++;

        UserInterface.AdjustScore(_score);
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    public void EndGame()
    {
        TogglePlayer(false);

        CancelInvoke();
        ObjectPoolManager.Instance.ResetObjects();
        UserInterface.EndGame(); //Adjust the UI
    }

}
