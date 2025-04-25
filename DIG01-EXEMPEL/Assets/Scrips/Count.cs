using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour
{
    int num = 1;
    void Start()
    {
        StartCoroutine(Counting());
    }

    void Update()
    {
        //while (num > 10)
        //{
        //    num++;
        //    Debug.Log(num);
        //}

        //while (num < 10)
        //{
        //    num--;
        //    Debug.Log(num);
        //}
    }

    IEnumerator Counting()
    {
        while (num < 10)
        {
            // Count to Ten
            num++;
            Debug.Log(num);
            yield return null;
        }

        while (num > 0)
        {
            // Count back to Zero
            num--;
            Debug.Log(num);
            yield return null;
        }
    }

}
