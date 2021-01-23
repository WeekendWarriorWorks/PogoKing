
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Button buttonLeft;

    [SerializeField]
    private Button buttonRight;

    private float horizontalMovement;
    private Rigidbody2D rb;
    private float moveSpeed = 5;

    private bool leftClickDown = false;
    private bool rightClickDown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        buttonLeft.GetComponent<Button>().onClick.AddListener(OnButtonLeftClick);

       // HideMouseCursor();
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
        if (horizontalMovement == 0)
        {
            horizontalMovement = Input.acceleration.x;
        }

        Vector2 movement = new Vector2(horizontalMovement, 0);
        if (movement.x != 0)
        {
            rb.AddForce(movement * moveSpeed, ForceMode2D.Force);
        }
    }

    private void OnButtonLeftClick()
    {
        Debug.Log("LEFT CLIKC");

    }

    private void ResetGame()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = new Vector3(0, 2, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("COLLISION");

        if (collision.collider.tag.Equals("DeathCollider"))
        {
            ResetGame();
            return;
        }

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
