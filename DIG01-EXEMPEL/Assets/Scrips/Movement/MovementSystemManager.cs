using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject infoTextKeyBoard;
    public int currentMovement = 1;

    void Update()
    {
        // Checking which number key is pressed
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            currentMovement = 1;
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            currentMovement = 2;
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            currentMovement = 3;
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
            currentMovement = 4;
        else if (Keyboard.current.digit5Key.wasPressedThisFrame)
            currentMovement = 5;
        else if (Keyboard.current.digit6Key.wasPressedThisFrame)
            currentMovement = 6;

        switch (currentMovement)
        {
            case 1:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Transfrom Translate");
                break;

            case 2:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Rigidbody AddForce");
                break;

            case 3:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Rigidbody MovePosition");
                break;

            case 4:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Rigidbody Velocity");
                break;

            case 5:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Vector2 MoveTowards");
                break;

            case 6:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Vector2 SmoothDamp");
                break;
        }
    }
}
