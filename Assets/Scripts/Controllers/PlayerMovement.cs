
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float horizMovement;
    private Rigidbody2D rb;
    private float moveSpeed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        HideMouseCursor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Determine horizontal movement
        horizMovement = Input.GetAxisRaw("Horizontal");
        // If no joystick/keyboard movement, check accelerometer
        if (horizMovement == 0)
        {
            horizMovement = -Input.acceleration.x;
        }

        // Only add force if horizontal movement was detected
        if (horizMovement != 0)
        {
            Vector2 movement = new Vector2(horizMovement, 0);
            rb.AddForce(movement * moveSpeed, ForceMode2D.Force);
        }
    }

    private void ResetGame()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = new Vector3(0, 2, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we collide with the bounding box at bottom of level, reset game
        if (collision.collider.tag.Equals("DeathCollider"))
        {
            ResetGame();
            return;
        }

        // Calculate dot product between direction of contact and player
        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, rb.transform.up.normalized) > 0.5)
        {
            // Add manual force to immitate pogo stick
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
