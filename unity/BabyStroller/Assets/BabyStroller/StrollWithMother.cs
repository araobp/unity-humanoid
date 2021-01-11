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
        transform.position = new Vector3(mother.transform.position.x, transform.position.y, mother.transform.position.z + distanceFromMother);
    }
}
