using UnityEngine;
using UnityEngine.InputSystem;

public class InputInteractions : MonoBehaviour
{
    public void OnHold(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Holding!");
        }
    }

    public void OnMultiTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Multi Tapped!");
        }
    }

    public void OnTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Tapped!");
        }
    }

    public void OnSlowTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Slow Tapped!");
        }
    }
}
