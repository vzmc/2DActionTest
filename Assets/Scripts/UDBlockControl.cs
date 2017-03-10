using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDBlockControl : MonoBehaviour
{
    public float moveSpeed = 2;
    public float moveDistance = 5;

    private Rigidbody2D rgbd2d;
    private Vector2 velocity;
    private float moved;

    // Use this for initialization
    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        velocity = new Vector2(moveSpeed, 0);
        moved = 0;
        rgbd2d.velocity = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moved > moveDistance)
        {
            rgbd2d.velocity = -rgbd2d.velocity;
            moved = 0;
        }
        moved += moveSpeed * (Time.deltaTime / 1.0f);
    }
}
