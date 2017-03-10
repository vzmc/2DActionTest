using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniControl : MonoBehaviour
{
    public float vSpeed = 5;    //ダメージ受けた時最初の縦速度
    public float hSpeed = 5;    //ダメージ受けた時の横速度
    public float gForce = -10;    //重力
    public Transform destroyTransform;

    private Animator animator;
    private CircleCollider2D colider;
    private bool isDead;
    private int damegeDirection;
    private Vector3 destroyPosition;

    // Use this for initialization
    void Start()
    {
        destroyPosition = destroyTransform.position;
        animator = GetComponent<Animator>();
        colider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (isDead)
        {
            GoDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Punch")
        {
            animator.SetTrigger("Damege");
            isDead = true;
            damegeDirection = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
            hSpeed *= damegeDirection;
            colider.enabled = false;
        }
    }

    private void GoDie()
    {
        transform.Translate(new Vector3(hSpeed, vSpeed, 0) * Time.deltaTime);
        vSpeed += gForce * Time.deltaTime;

        if(transform.position.y < destroyPosition.y)
        {
            Destroy(gameObject);
        }
    }
}
