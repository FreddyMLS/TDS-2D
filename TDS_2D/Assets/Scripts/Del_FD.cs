using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Del_FD : MonoBehaviour
{
    private GameObject text;

    void Update()
    {
        text = GameObject.FindGameObjectWithTag("text");
        if (text == null)
        {
            Destroy(gameObject);
        }
    }
}
