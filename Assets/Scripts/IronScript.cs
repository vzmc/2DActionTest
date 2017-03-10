using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    private Vector3 damageFrom;
    private float damageForce;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Punch")
        {
            damageFrom = collision.transform.position;
            GetDamage(damageFrom, 1000);
        }
    }

    public void GetDamage(Vector3 damageFrom, float force)
    {
        this.damageFrom = damageFrom;
        damageForce = force;
        rd2d.AddForce((transform.position - damageFrom).normalized * damageForce, ForceMode2D.Impulse);
    }
}
