using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Objects to spawn
    public GameObject[] objects;

    // Player Transform
    private Transform playerTrans;

    // Player position
    private Vector3 playerPos;

    // Spawn position offset
    private Vector3 offset;

    // Spawn position
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize playerTrans
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();

        // Initialize offset
        offset = new Vector3(3, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // SpawnObject(); 
    }

    // Method to spawn each object
    private void SpawnObject()
    {
        // Calculate playerPos
        playerPos = playerTrans.position;

        // Spawn position
        spawnPos = playerPos + offset;

        // If 0 is pressed, spawn straight line enemy
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            // Spawn straight line enemy
            Instantiate(objects[0], spawnPos, objects[0].transform.rotation);
        }

        // Else if 1 is pressed, spawn flying enemy
        else if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            // Spawn flying enemy
            Instantiate(objects[1], spawnPos, objects[1].transform.rotation);
        }

        // Else if 2 is pressed, spawn bouncing enemy
        else if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            // Spawn bouncing enemy
            Instantiate(objects[2], spawnPos, objects[2].transform.rotation);
        }

        // Else if 3 is pressed, spawn a coin
        else if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            // Spawn a coin
            Instantiate(objects[3], spawnPos, objects[3].transform.rotation);
        }
    }
}
