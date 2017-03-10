using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject Bullet;
    private CharacterAction ca;

    private void Start()
    {
        ca = GetComponentInParent<CharacterAction>();
    }

    public void OnShoot()
    { 
        GameObject b = Instantiate(Bullet, transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = new Vector2((int)ca.GetDirection() * 10, 0);
    }
}
