//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.Users;

//public class InputManager : MonoBehaviour
//{
//    public GameObject keyboardPrefab;
//    public GameObject mousePrefab;
//    public GameObject gamepadPrefab;

//    void Start()
//    {
//        // Instantiate and automatically assign devices to the correct prefab
//        foreach (var device in InputSystem.devices)
//        {
//            if (device is Keyboard)
//                PlayerInput.Instantiate(keyboardPrefab,"Keyboard", pairWithDevice: device);
//            else if (device is Mouse)
//                PlayerInput.Instantiate(mousePrefab,"Mouse", pairWithDevice: device);
//            else if (device is Gamepad)
//                PlayerInput.Instantiate(gamepadPrefab,"Gamepad", pairWithDevice: device);
//        }
//    }
//}