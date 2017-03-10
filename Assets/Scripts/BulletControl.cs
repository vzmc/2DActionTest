using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject Effect;
    // Use this for initialization

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player" || collision.gameObject.tag != "Coin")
        {
            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
