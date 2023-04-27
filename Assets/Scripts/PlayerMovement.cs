using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public GameObject tilemap;
    public GameObject endCanvas;
    public GameObject doorLocked;
    public GameObject instructions;
    public GameObject goldenGem;
    public Text gemsRemaining;
    public int gems;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    int score = 0;
    bool locked = false;
    bool idle = false;

    void Start()
    {
        gemsRemaining.text = "Gems Remaining: " + gems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!idle)
        {
            locked = false;

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && controller.m_CrouchDisableCollider.enabled == true)
            {
                    jump = true;
                    animator.SetBool("IsJumping", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }

            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
        else
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                Debug.Log("Hello");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            horizontalMove = 0;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            crouch = false;
        }
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate ()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem") && !locked)
        {
            locked = true;
            Destroy(other.gameObject);
            score += 1;
            if (gems == score)
            {
                gemsRemaining.text = "All Gems Collected!";
                gemsRemaining.fontSize = 28;
                goldenGem.SetActive(true);
            }
            else
            {
                gemsRemaining.text = "Gems Remaining: " + (gems-score).ToString();
            }
        }
        else if ((other.gameObject.CompareTag("Spike") || other.gameObject.CompareTag("Eagle") || other.gameObject.CompareTag("OOB") || other.gameObject.CompareTag("Bottom Frog"))  && !locked)
        {
            locked = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.gameObject.CompareTag("Top Frog") && !locked)
        {
            locked = true;
            Destroy(other.transform.parent.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Opossum") && !locked)
        {
            locked = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.gameObject.CompareTag("Door") && !locked)
        {
            locked = true;
            if (gems == score)
            {
                idle = true;
                gemsRemaining.text = "";
                tilemap.SetActive(true);
                endCanvas.SetActive(true);
                other.gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                doorLocked.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Sign") && !locked)
        {
            locked = true;
            instructions.SetActive(true);
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door") && !locked && gems != score)
        {
            locked = true;
            doorLocked.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Sign") && !locked)
        {
            locked = true;
            instructions.SetActive(false);
        }
    }
}
