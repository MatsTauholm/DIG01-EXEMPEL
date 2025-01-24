using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInfoText : MonoBehaviour
{
    //string text;
    TextMesh textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    public void UpdateText(string text)
    {
        textMesh.text = text;
    }
}
