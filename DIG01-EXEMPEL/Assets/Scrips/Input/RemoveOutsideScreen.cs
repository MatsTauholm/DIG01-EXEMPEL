using UnityEngine;

public class RemoveOutsideScreen : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
