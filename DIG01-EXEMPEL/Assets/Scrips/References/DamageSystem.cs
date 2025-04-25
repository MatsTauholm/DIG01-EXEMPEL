using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public AudioClip hurtSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    void TakeDamage()
    {
        SoundManager.PlaySound(hurtSound);
    }
}
