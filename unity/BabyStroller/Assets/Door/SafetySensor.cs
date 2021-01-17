using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetySensor : MonoBehaviour
{
    public bool enabled = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (enabled)
        {
            gameObject.GetComponentInParent<Animator>().SetTrigger("Open");
        } else
        {
            StartCoroutine(DisableRigidbody());
        }
    }

    IEnumerator DisableRigidbody()
    {
        yield return new WaitForSeconds(1);
        GetComponentInParent<Rigidbody>().isKinematic = true;
    }
}
