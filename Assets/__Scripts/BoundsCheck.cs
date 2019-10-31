﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool isOnScreen = true;                                      // b
    public float camWidth;
    public float camHeight;
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;                       // a


    void Awake()
    {
        camHeight = Camera.main.orthographicSize;                            // b
        camWidth = camHeight * Camera.main.aspect;                           // c
    }

    void LateUpdate()
    {                                                     // d
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;                       // b

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;                                                // c
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;                                                 // c
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;                                                   // c
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;                                                 // c
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);            // d
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;                   // e
        }
    }


    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos()
    {                                                    // e
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3 (camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}