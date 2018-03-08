using UnityEngine;
using System.Collections;

/// <summary>
/// Handles enemy AI specific behaviors such as moving, shooting and taking damage
/// </summary>
public class Enemy : Ship {

    private GameObject _player;
    private float _distanceToPlayer = 2.5f;

    private Coroutine movementCoroutine;

    private bool isMoving = false;
    private Vector3 _endPos;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindObjectOfType<Player>().gameObject;
        }

        SetupEnemy(transform.position);
    }

    private void OnDisable()
    {
        isMoving = false;
    }
    public void SetupEnemy(Vector3 spawnPosition)
    {
        if (_player == null)
        {
            _player = GameObject.FindObjectOfType<Player>().gameObject;
        }

        //reset hit points
        HitPoints = 2;

        transform.position = spawnPosition;

        movementCoroutine = StartCoroutine(GetNewPosition());
    }

    void Update() {

        if (isMoving)
        {
            Vector3 direction = transform.position - _endPos;
            transform.position = transform.position - direction.normalized * .02f;

        }

        if (CanShoot)
        {
            CanShoot = false;

            ShootLaser();
        }
    }

    IEnumerator GetNewPosition()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.25f);
        bool navigateToPlayer = true;
        isMoving = true;

        while (navigateToPlayer)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) > 3.5)
            {
                _endPos = _player.transform.position + Vector3.up * _distanceToPlayer + ((Vector3)Random.insideUnitCircle * 1);
                Debug.Log("still navigating to player");
            }
            else
            {
                Debug.Log("leaving the game");
                navigateToPlayer = false;
                _endPos = new Vector3(0f, -10f, 0f) + ((Vector3)Random.insideUnitCircle * 3);
            }
            yield return waitTime;
        }
    }

    public override void CalculateHit(int amount)
    {
        base.CalculateHit(amount);

        if (HitPoints <= 0)
        {
            GameManager.Instance.IncrementScore();
            isMoving = false;
            gameObject.SetActive(false);
        }
    }
}
