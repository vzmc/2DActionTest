using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinControl : MonoBehaviour
{
    private bool isGot = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGot)
        {
            transform.Translate(new Vector3(0, 1, 0) * 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGot)
        {
            return;
        }
        if(collision.gameObject.tag == "Player")
        {
            isGot = true;
            animator.SetTrigger("GotTrigger");
            ScoreControl.AddScore(1);
        }
    }

    public void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}
