using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrollWithMother : MonoBehaviour
{

    public GameObject mother;
    public float distanceFromMother = 0.5F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = mother.transform.position + mother.transform.forward * distanceFromMother;

        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        transform.Rotate(transform.up, mother.transform.rotation.y - transform.rotation.y);
    }
}
