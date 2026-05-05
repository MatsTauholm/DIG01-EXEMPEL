using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect = 1f; //0 = no movement, 1 = same movement as camera

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //Get the width of the background sprite
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect; //Calculate the distance the background should move based on the camera's position and the parallax effect factor
        float movement = cam.transform.position.x * (1 - parallaxEffect); //Calculate the movement of the background based on the camera's position and the parallax effect factor
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + length) //If the background has moved too far to the right, reset its position to the left
        {
            startPos += length;
        }
        else if (movement < startPos - length) //If the background has moved too far to the left, reset its position to the right
        {
            startPos -= length;
        }
    }
}
