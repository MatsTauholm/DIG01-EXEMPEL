using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] string goToLevel;
    [SerializeField] float newPosX;
    [SerializeField] float newPosY;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(goToLevel);
            FindObjectOfType<PlayerMovement>().SetStartPos(newPosX,newPosY);
        }
    }
}
