﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResize : MonoBehaviour
{
    private float size;
    private float resize;
    private float speed;

    private Rigidbody2D playerRigidBody;
    public float maxResize = 10f;

    void Start()
    {
        playerRigidBody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
        speed = 1f;
        size = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (playerRigidBody)
        {
            resize = size + 1f * Mathf.Abs(playerRigidBody.velocity.x);
            if (resize >= maxResize)
            {
                resize = 8f;
            }
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, resize, speed * Time.deltaTime);
        }
    }
}
