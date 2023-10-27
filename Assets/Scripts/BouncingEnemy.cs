using UnityEngine;

// INHERITANCE
public class BouncingEnemy : Enemy
{
    // Ground state
    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize isOnGround
        isOnGround = false;

        Initializations();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Destruction();
    }

    // Move Method
    // POLYMORPHISM
    protected override void Move()
    {
        // Rectangle's movement : bounce to the left , as long as the game is not stopped and the enemy is not defeated
        if(!lvlManagerScript.isLvlFinished && !lvlManagerScript.isGameStopped && !isDefeated)
        {
            Bounce();
        }
    }

    private void Bounce()
    {
        // jumpInterval decreases
        jumpInterval -= Time.deltaTime;

        // If enemy is on ground and jumpInterval is below 0, the enemy bounces
        if(isOnGround && jumpInterval < 0)
        {
            enemyRb.AddForce(jumpVector, ForceMode2D.Impulse);

            jumpInterval = 3;
        }
    }

    // CollisionEnter2D Method
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If enemy collides with the ground, display collision object and switch isOnGround to true
        if(other.gameObject.tag == "Platform")
        {
            isOnGround = true;
        }

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

    // CollisionExit2D Method
    private void OnCollisionExit2D(Collision2D other)
    {
        // If enemy leaves the ground, display collision object and switch isOnGround to false
        if(other.gameObject.tag == "Platform")
        {
            isOnGround = false;
        }
    }
}
