using UnityEngine;

public class MovementSystemManager : MonoBehaviour
{
    public int currentMovement = 1;
      
    void Update()
    {
        ChangeMovementSystem();
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
