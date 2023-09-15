using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countLives;
    //public GameObject winTextObject;
    public Transform respawnPoint;
    public int lives;


    public MenuController menuController;
    private AudioSource music;

    private float movementX;
    private float movementY;

    private Rigidbody rb;
    [SerializeField]
    private int count;

    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        music=GetComponent<AudioSource>();
        // Set the count to zero 
        count = 0;
        lives = 5;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        //winTextObject.SetActive(false);
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            Respawn();
            lives--;
        }
        if (lives <= 0)
        {
            EndGame();
            Respawn();
        }
    }

    void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count++;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
            music.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           // Respawn();
           EndGame();
           count = 0;
           SetCountText(); 
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            // Set the text value of your 'winText'
            //winTextObject.SetActive(true);
            menuController.WinGame();
            count = 0;
        }
        //countLives.text = "Lives: " + lives.ToString();
        //if (lives <=0) {
        //   menuController.LoseGame();
        //}
    }
    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity=Vector3.zero;
        rb.Sleep();
        transform.position=respawnPoint.position;
    }
    void EndGame()
    {
        menuController.LoseGame();
        gameObject.SetActive(false);
    }

}
