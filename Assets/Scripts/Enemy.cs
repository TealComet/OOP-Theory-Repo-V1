using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Reference to Level Manager Script
    protected LevelManager lvlManagerScript;

    // Reference to Player Script
    protected Player playerScript;

    // Check if enemy is defeated
    protected bool isDefeated;

    // Speed
    protected float speed;

    // Scale in 3 dimensions
    protected float scaleX;
    protected float scaleY;
    protected float scaleZ;

    // X position limit
    protected float xLimit;

    // Y position limit
    protected float yLimit;

    // BOUNCING ENEMY VARIABLES

    // Rididbody2D
    protected Rigidbody2D enemyRb;

    // Time between two jumps
    protected float jumpInterval;

    // Horizontal jump force
    protected float jumpForceX;

    // Vertical jump force
    protected float jumpForceY;

    // Jump vector
    protected Vector3 jumpVector;


    // Start is called before the first frame update
    void Start()
    {
        // ABSTRACTION
        Initializations();
    }

    protected void Initializations()
    {
        // Initialize lvlManagerScript
        lvlManagerScript = FindObjectOfType<LevelManager>();

        // Initialize playerScript
        playerScript = FindObjectOfType<Player>();

        // Initialize isDefeated
        isDefeated = false;

        // Initialize speed
        speed = 2;

        // Initialize scale variables
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;

        // Initialize xLimit
        xLimit = 200;

        // Initialize yLimit
        yLimit = 20;

        // Initialize scale
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

        // Initialize enemyRb
        enemyRb = GetComponent<Rigidbody2D>();

        // Initialize jump interval and forces
        jumpInterval = 3;
        jumpForceX = -2;
        jumpForceY = 4;

        // Initialize jump vector
        jumpVector = new Vector3(jumpForceX, jumpForceY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        Move();
        Destruction();
    }

    // Move Method
    protected virtual void Move()
    {
        // Square's movement : straight to the left , as long as the game is not stopped and the enemy is not defeated
        if(!lvlManagerScript.isLvlFinished && !lvlManagerScript.isGameStopped && !isDefeated)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    // Collision Method
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the enemy collides with the player, the enemy is below, and the enemy is not defeated yet, the enemy gets squashed, player gains score, and the enemy is defeated
        if(!isDefeated && other.gameObject.CompareTag("Player") && transform.position.y < other.transform.position.y)
        {
            // Player's score increase
            playerScript.score += 100;

            // Enemy gets squashed
            transform.localScale = new Vector3(scaleX, scaleY/2, scaleZ);

            // Switch isDefeated to true
            isDefeated = true;
        }
    }

    // Destruction Method
    protected void Destruction()
    {
        // If enemy is defeated, destroy it after a few seconds
        if(isDefeated)
        {
            Destroy(gameObject, 3);
        }

        // If enemy is above positive limits or below negative limits, destroy it
        else if(transform.position.x > xLimit || transform.position.x < -xLimit || transform.position.y > yLimit || transform.position.y < -yLimit)
        {
            Destroy(gameObject);
        }
    }
}
