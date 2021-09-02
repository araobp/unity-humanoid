using System.Collections.Generic;
using UnityEngine;

public class WalkAlongMarkers : MonoBehaviour
{
    public int markerStart = 1;
    public int markerEnd = 4;

    public float markerDistance = 1F;
    public float sensitivity = 0.5F;

    List<Transform> markers = new List<Transform>();

    private Vector3 forward_a;
    private Vector3 forward_b;
    private float t = 0F;

    int idx = 0;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        GameObject o = GameObject.FindGameObjectWithTag("Markers");
        for (int num = markerStart; num <= markerEnd; num++)
        {
            markers.Add(o.transform.Find($"Marker{num}"));
        }

        var markerPos = markers[0].position;
        var direction = markerPos - transform.position;
        forward_b = new Vector3(direction.x, 0, direction.z);
        forward_a = forward_b;
        transform.forward = forward_a;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * sensitivity;
        var forward = Vector3.Lerp(forward_a, forward_b, t);
        transform.forward = forward;

        var distance = (markers[idx].position - transform.position).magnitude;

        if (distance < markerDistance)
        {
            if (idx < (markers.Count - 1))
            {
                idx++;
                var markerPos = markers[idx].position;
                var direction = markerPos - transform.position;
                forward_a = forward_b;
                forward_b = new Vector3(direction.x, 0, direction.z);
                t = 0F;
            }
            else
            {
                animator.SetTrigger("Stay");
            }
        }
    }

    public void Run()
    {
        animator.SetTrigger("Run");
    }
}