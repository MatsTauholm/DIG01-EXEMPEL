using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint; //All the spawn points for the players are stored in this array, which is set in the inspector.
    private int playerCount;

    //When a player presses a button on a input device, this method is called by the Player Input Manager and the player is instanceiated.
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.position = spawnPoint[playerCount].position; //The player is moved to the position of the spawn point corresponding to the current player count.
        playerCount++;
    }
}
