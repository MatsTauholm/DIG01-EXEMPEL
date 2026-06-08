using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputInteractions : MonoBehaviour
{
    public void OnFire(InputAction.CallbackContext context)
    {
        // SlowTap starts charging when the button is pressed
        if (context.started)
        {
            Debug.Log("Started charging!");
        }

        // A Tap interaction completed
        if (context.performed &&
            context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            Debug.Log("Performed a Tap!");
        }

        // A SlowTap interaction completed
        if (context.performed &&
            context.interaction is UnityEngine.InputSystem.Interactions.SlowTapInteraction)
        {
            Debug.Log("Performed a Slow Tap!");
        }

        // Button released before completing a SlowTap
        if (context.canceled)
        {
           Debug.Log("Stopped charging");
        }
    }

    public void OnHold(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Holding!");
        }

        if (ctx.canceled)
        {
            Debug.Log("Held for " + ctx.duration + " seconds.");
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
