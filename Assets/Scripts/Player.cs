using UnityEngine;
using TMPro;

// Link to my repository : https://github.com/TealComet/OOP-Theory-Repo-V1

public class Player : MonoBehaviour
{
    // LevelManager Script
    private LevelManager lvlManagerScript;

    // Rigidbody2D
    private Rigidbody2D playerRb;

    // Axis
    private float horiInput;

    // Move Speed
    private float speed = 4;

    // Jump force
    private float jumpForce = 5;

    // Y Position below which ResetYPosition() is called
    private float yLimit = -10;

    // Life Number
    public int lifeNum = 3;

    // Score Value
    public int score;

    // Score Text
    private TextMeshProUGUI scoreText;

    // Heart Objects
    private GameObject[] hearts;

    // Ground State
    private bool isOnGround;

    // Force with which the player is knocked back
    private float knockbackForce = 4;

    // Checking if the player has been hit
    private bool isHit;

    // Time of invulnerability
    private int invulnerableTimer;

    // Player Sprite Renderer
    private SpriteRenderer playerRend;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize lvlManagerScript
        lvlManagerScript = FindObjectOfType<LevelManager>();

        // Initialize isOnGround
        isOnGround = false;

        // Initialize playerRb
        playerRb = GetComponent<Rigidbody2D>();

        // Initialize hearts
        hearts = GameObject.FindGameObjectsWithTag("Heart");

        // Initialize score
        score = 0;

        // Initialize scoreText
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        scoreText.text = $"Score {score:0000}";

        // Initialize isHit
        isHit = false;

        // Initialize invulnerableTimer
        invulnerableTimer = 200;

        // Initialize playerRend
        playerRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        Move();
        Jump();
        ResetYPosition();
        Knockback();
    }

    // Move Method
    private void Move()
    {
        // Activate axis
        horiInput = Input.GetAxisRaw("Horizontal");

        // As long as the game is not fnished nor paused, the player can move
        if(!lvlManagerScript.isLvlFinished && !lvlManagerScript.isGameStopped)
        {
            // X Axis movement by left and right arrow keys
            transform.Translate(Vector3.right * horiInput * speed * Time.deltaTime);
        }
    }

    // Jump Method
    private void Jump()
    {
        // As long as the game is not paused and player is on the ground and presses the spacebar, they can jump
        if(!lvlManagerScript.isGameStopped && isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    // Reset Y Position Method
    private void ResetYPosition()
    {
        if(transform.position.y < yLimit && lifeNum > 0)
        {
            // Position player above void
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            // Player loses a life if they have more than 0
            LifeLoss();
        }
    }

    // Collision Enter2D
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If player collides with a platform while above it, switch isOnGround to true
        if(other.gameObject.tag == "Platform" && transform.position.y > other.gameObject.transform.position.y)
        {
            isOnGround = true;
        }

        // Else if the player is not hit and collides with an enemy and is below or equal to it and player has more than 0 lives, they lose 1 life and a heart sprite is destroyed
        else if(!isHit && other.gameObject.tag == "Enemy" && transform.position.y <= other.gameObject.transform.position.y && lifeNum > 0)
        {
            // If player is to the left of the enemy, they are sent to the left
            if(transform.position.x <= other.gameObject.transform.position.x)
            {
                // Give leftward force to the player
                playerRb.AddForce(Vector3.left * knockbackForce, ForceMode2D.Impulse);
            }

            // If player is to the right of the enemy, they are sent to the left
            else if(transform.position.x >= other.gameObject.transform.position.x)
            {
                // Give leftward force to the player
                playerRb.AddForce(Vector3.right * knockbackForce, ForceMode2D.Impulse);
            }

            // Player loses a life if they have more than 0
            LifeLoss();

            // Switch isHit
            isHit = true;
        }

        // Else if the player collides with an enemy and the player is above enemy, the player bounces up and their score value is increased
        else if(other.gameObject.tag == "Enemy" && transform.position.y > other.gameObject.transform.position.y && lifeNum > 0)
        {
            // Give upward force to the player
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

            // Update Score Text
            scoreText.text = $"Score {score:0000}";
        }
    }

    // Collision Exit2D
    private void OnCollisionExit2D(Collision2D other)
    {
        // If player leaves a platform while above it, switch isOnGround to false
        if(other.gameObject.tag == ("Platform") && transform.position.y > other.gameObject.transform.position.y)
        {
            isOnGround = false;
        }
    }

    // Trigger Enter2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player triggers a coin, they gain points and the coin is destroyed
        if(other.gameObject.tag == ("Coin"))
        {
            // Player gains points
            score += 50;

            // Update score text
            scoreText.text = $"Score {score:0000}";

            // Destroy coin
            Destroy(other.gameObject);
        }

        // If player triggers a flag, call lvlManagerScript's Victory method
        else if(other.gameObject.tag == "Flag")
        {
            lvlManagerScript.Victory();
        }
    }

    // Knockback Method
    private void Knockback()
    {
        // If the player is hit and invulnerableTimer is above 0, the player blinks
        if(isHit && invulnerableTimer > 0)
        {
            invulnerableTimer--;

            // If invulnerableTimer is even, the player is visible
            if(invulnerableTimer % 2 == 0)
            {
                playerRend.enabled = true;
            }

            // If invulnerableTimer is not even, the player is invisible
            else
            {
               playerRend.enabled = false; 
            }
        }

        // If invulnerableTimer goes below 0, isHit is switched and invulnerableTimer is reset
        else if(invulnerableTimer <= 0)
        {
            isHit = false;
            invulnerableTimer = 200;
        }
    }

    // Life loss method
    private void LifeLoss()
    {
        // If player has more than 0 lives, they lose 1 life
        if(lifeNum > 0)
        {
            // Assign Last Heart
            GameObject lastHeart = hearts[lifeNum-1].gameObject;

            // Destroy Last Heart
            Destroy(lastHeart);

            // Decrease Life Number
            lifeNum--;
        }
    }
}
