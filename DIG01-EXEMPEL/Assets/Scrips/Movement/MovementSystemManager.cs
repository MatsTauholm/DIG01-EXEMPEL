using UnityEngine;

public class MovementSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject infoTextKeyBoard, infoTextMouse;
    public int currentMovement = 1;
    
    void Update()
    {
        ChangeMovementSystem();

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

    private void ChangeMovementSystem()
    {
        //Checking whitch numberkey is pressed
        for (int i = 0; i <= 9; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                currentMovement = i;
            }
        }
    }
}
