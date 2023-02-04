using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EagleMovement : MonoBehaviour
{
    public EagleController2D controller;
    public float distance = 10f;
    public float speed = 25f;

    private float startPosition;
    private float verticalMove = 0f;
    
    void Start ()
    {
        verticalMove = speed;
        startPosition = transform.position.y;
    }
   
    void Update ()
    {
        if (transform.position.y > startPosition + distance)
        {
            verticalMove = speed * -1;
        }
        else if (transform.position.y < startPosition)
        {
            verticalMove = speed;
        } 
    }

    void FixedUpdate ()
    {
        controller.Move(verticalMove * Time.fixedDeltaTime);
    }

    void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
         Gizmos.DrawLine(transform.position, transform.position + Vector3.up * (distance + 1.86f));
    }
 }