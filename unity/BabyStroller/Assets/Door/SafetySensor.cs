using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetySensor : MonoBehaviour
{
    public bool enabled = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (enabled)
        {
            gameObject.GetComponentInParent<Animator>().SetTrigger("Open");
        }
    }
}
