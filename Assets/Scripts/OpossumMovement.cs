using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpossumMovement : MonoBehaviour
{
    public OpossumController2D controller;
    public float distance = 5f;
    public float speed = 20f;

    private float startPosition;
    private float horizontalMove = 0f;
    
    void Start ()
    {
        horizontalMove = speed;
        startPosition = transform.position.x;
    }
   
    void Update ()
    {
        if (transform.position.x > startPosition + distance)
        {
            horizontalMove = speed * -1;
        }
        else if (transform.position.x < startPosition)
        {
            horizontalMove = speed;
        } 
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);
    }

    void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
         Gizmos.DrawLine(transform.position, transform.position + Vector3.right * (distance + 1.98f));
    }
 }
