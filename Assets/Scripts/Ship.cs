using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Ship : MonoBehaviour {

    [Header("General Properties")]
    public int BaseHitPoints = 3;
    public int CurrentHitPoints = 3;
    public float FireDelay;
    [Range(.01f,15)]
    public float Speed;


    [Header("Laser Properties")]
    public float LaserDistanceForward;
    public float LaserDistanceRight;
    public Vector3 LaserRotation;

    [Header("Other")]
    protected bool CanShoot = true;
    protected EnvrionmentManager EnvironmentMngr;


    // Use this for initialization
    void Start()
    {
        EnvironmentMngr = GameObject.FindObjectOfType<EnvrionmentManager>();
    }

    /// <summary>
    /// Creates an instance of the laser game object and disables firing based on the FireDelay
    /// </summary>
    protected void ShootLaser()
    {
        GameObject laser = tag.Contains("Player") ? ObjectPoolManager.Instance.RequestObject(ObjectPoolManager.ObjectType.PlayerLaser) : 
            ObjectPoolManager.Instance.RequestObject(ObjectPoolManager.ObjectType.EnemyLaser);

        Vector3 laserSpawnPosition = new Vector3(transform.position.x + LaserDistanceRight, transform.position.y + 1 * LaserDistanceForward);
        laser.transform.position = laserSpawnPosition;
        laser.transform.rotation = Quaternion.Euler(LaserRotation);
        laser.SetActive(true);
        Invoke("EnableShooting", FireDelay);
    }


    /// <summary>
    /// Enables the ability to shoot
    /// </summary>
    protected void EnableShooting()
    {
        CanShoot = true;
    }

    /// <summary>
    /// Applies the damage of the bullet/laser to the health and applies death
    /// </summary>
    /// <param name="amount">Amount of damage to apply</param>
    public virtual void CalculateHit(int amount)
    {
        CurrentHitPoints -= amount;
    }
}
