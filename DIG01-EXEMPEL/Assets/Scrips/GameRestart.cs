using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }       
    }
}
