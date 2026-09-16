using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject infoTextKeyBoard, infoTextMouse;
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

        switch (currentMovement)
        {
            case 1:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Transfrom Translate");
                infoTextMouse.GetComponent<ChangeInfoText>().UpdateText("Vector2 MoveTowards");
                break;

            case 2:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Rigidbody AddForce");
                infoTextMouse.GetComponent<ChangeInfoText>().UpdateText("Vector2 SmoothDamp");
                break;

            case 3:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Rigidbody MovePosition");
                break;

            case 4:
                infoTextKeyBoard.GetComponent<ChangeInfoText>().UpdateText("Rigidbody Velocity");
                break;
        }
    }
}
