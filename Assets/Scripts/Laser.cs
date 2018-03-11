using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public float Speed = .04f;
    public int Damage = 10;
    public bool EnemyWeapon;

    // Update is called once per frame
    void Update()
    {
        if (EnemyWeapon)
        {
            transform.position = transform.position - Vector3.up * Speed;
        }
        else
        {
            transform.position = transform.position + Vector3.up * Speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Test OnCollisionEnter2D");

        if (!EnemyWeapon && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().CalculateHit(Damage);
            gameObject.SetActive(false);
        }

        else if (EnemyWeapon && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().CalculateHit(Damage);
            gameObject.SetActive(false);
        }

    }

}
