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
            SpawnPlayer(0);
            wasdJoined = true;
        }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame && !arrowJoined)
        {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "Keyboard",
                pairWithDevice: Keyboard.current);

            arrowJoined = true;
        }
        else if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame && !gamepadJoined)
        {
            SpawnPlayer(2);
            gamepadJoined = true;
        }
    }
}
