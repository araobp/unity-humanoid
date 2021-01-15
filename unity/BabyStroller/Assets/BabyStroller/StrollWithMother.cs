using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrollWithMother : MonoBehaviour
{

    public GameObject mother;
    public float distanceFromMother = 0.5F;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = mother.transform.position + mother.transform.forward * distanceFromMother;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mother.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
