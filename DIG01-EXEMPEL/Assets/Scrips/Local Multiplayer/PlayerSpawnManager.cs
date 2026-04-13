using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    private bool wasdJoined;
    private bool arrowJoined;
    private bool gamepadJoined;

    void Update()
    {
       if (Keyboard.current.wKey.wasPressedThisFrame && !wasdJoined)
        {
              var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "WASD",
                pairWithDevice: Keyboard.current);
                if (spawnPoints.Length > 0)
                {
                    player.transform.position = spawnPoints[0].position;
                }
            wasdJoined = true;
        }

         if (Keyboard.current.upArrowKey.wasPressedThisFrame && !arrowJoined)
        {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "Arrows",
                pairWithDevice: Keyboard.current);

            if (spawnPoints.Length > 1)
            {
                player.transform.position = spawnPoints[1].position;
            }
            arrowJoined = true;
        }

        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !gamepadJoined)
            {
                var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "Gamepad",
                pairWithDevice: gamepad);
            
                 if (spawnPoints.Length > 2)
                {
                    player.transform.position = spawnPoints[2].position;
                }
                gamepadJoined = true;
            }
        }
    }
}
