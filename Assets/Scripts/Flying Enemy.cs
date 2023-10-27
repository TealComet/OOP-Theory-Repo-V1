using System; // Necessary to use Math.PI
using UnityEngine;

public class FlyingEnemy : Enemy
{
    // f(x) = a * sin(d(x + h)) + m
    // P = 2π/d

    // Amplitude
    private float amplitude;

    // Horizontal Distance between two maximums/minimums
    private float distance;

    // Horizontal Distance Multiplier
    private float multiplier;

    // Self incrementing variable
    private float autocount;

    // Midline
    private float midline;

    // X position
    private float xPos;

    // Y position
    private float yPos;

    // Position Vector
    private Vector3 enemyPos;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize amplitude
        amplitude = 0.4f;

        // Initialize autocount
        autocount = 0;

        // Initialize midline
        midline = transform.position.y;

        // Initialize multiplier
        multiplier = 0.1f;

        // Initialize distance (P = 2π/d)
        distance = (float) Math.PI * multiplier;

        // Initialize xPos
        xPos = transform.position.x;

        Initializations();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Destruction();
    }

    // Move Method
    protected override void Move()
    {
        // xPos self decrements
        xPos -= 0.3f * Time.deltaTime;

        // autocount self increments
        autocount += 10f * Time.deltaTime;

        // yPos is recalculated to account for the change in autocount
        yPos = amplitude * Mathf.Sin(distance * (autocount)) + midline;

        // enemyPos is recalculated to account for the change in autocount
        enemyPos = new Vector3(xPos, yPos, 0);

        // Triangle's position : zigzag to the left , as long as the game is not stopped and the enemy is not defeated
        if(!lvlManagerScript.isLvlFinished && !lvlManagerScript.isGameStopped && !isDefeated)
        {
            transform.position = enemyPos;
        }
    }
}
