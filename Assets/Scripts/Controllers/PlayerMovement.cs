using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMovement;
    private float verticalMovement;
    private Rigidbody2D rb;
    private float moveSpeed = 5;
    private bool jump = false;
    private float jumpForce = 5;
    private float nextJump = 0;
    private float timeBetweenJump = 1;
    private float rotationSpeed = 75;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        HideMouseCursor();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            jump = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        // Determine horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(horizontalMovement, 0);
        if (movement.x != 0)
        {
            rb.AddForce(movement * moveSpeed, ForceMode2D.Force);
        }

        /*
        // Determine rotation left
        float mouseX = Input.GetAxisRaw("Mouse X");
        if (mouseX != 0)
        {
            rb.transform.Rotate(0f, 0f, -mouseX * rotationSpeed * Time.deltaTime, Space.Self);
        }

        // Determine rotation right
       // if (mouseX > 0)
       // {
       //     rb.transform.Rotate(0f, 0f, -rotaonSpeed * Time.deltaTime, Space.Self);
       // }
        */

        if (jump)
        {
            jump = false;
            TimedJump();
        }
    }

    private void TimedJump()
    {
        if (Time.time > nextJump)
        {
            nextJump = Time.time + timeBetweenJump;
            AddJumpForce();
        }
    }

    private void AddJumpForce()
    {
       // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("hey");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, rb.transform.up.normalized) > 0.5)
        {
            rb.AddForce(rb.transform.up.normalized * 10, ForceMode2D.Impulse);
        }
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnApplicationQuit()
    {
        ShowMouseCursor();
    }
}
