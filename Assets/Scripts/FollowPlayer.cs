using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Player Transform
    private Transform playerTrans;

    // Player position
    private Vector3 playerPos;
    
    // Start is called before the first frame update
    void Start()
    {  
        // Initialize playerTrans
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowingPlayer();
    }

    // Method to always follow player in X and Y axis
    private void FollowingPlayer()
    {
        // Calculate playerPos
        playerPos = playerTrans.position;

        // Calculate Camera position according to player position
        transform.position = new Vector3(playerPos.x, playerPos.y+3.5f, transform.position.z);
    }
}
