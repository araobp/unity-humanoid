using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public int m_MarkerStart = 1;
    public int m_MarkerEnd = 2;

    public float m_MarkerDistance = 1F;
    public float m_Sensitivity = 2F;

    List<Transform> m_Markers = new List<Transform>();

    private Vector3 m_Forward_a;
    private Vector3 m_Forward_b;
    private float m_T = 0F;
        
    int m_Idx = 0;

    Vector3 m_OriginalPosition;

    Animator m_Animator;

    bool m_ControlForward = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_OriginalPosition = transform.position;

        GameObject o = GameObject.FindGameObjectWithTag("Markers");
        for (int num = m_MarkerStart; num <= m_MarkerEnd; num++)
        {
            m_Markers.Add(o.transform.Find($"Marker{num}"));
        }

        var markerPos = m_Markers[0].position;
        var direction = markerPos - transform.position;
        m_Forward_b = new Vector3(direction.x, 0, direction.z);
        m_Forward_a = m_Forward_b;
        transform.forward = m_Forward_a;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ControlForward)
        {
            m_T += Time.deltaTime * m_Sensitivity;
            var forward = Vector3.Lerp(m_Forward_a, m_Forward_b, m_T);
            transform.forward = forward;

            var distance = (m_Markers[m_Idx].position - transform.position).magnitude;

            if (distance < m_MarkerDistance)
            {
                if (m_Idx < (m_Markers.Count - 1))
                {
                    m_Idx++;
                    var markerPos = m_Markers[m_Idx].position;
                    var direction = markerPos - transform.position;
                    m_Forward_a = m_Forward_b;
                    m_Forward_b = new Vector3(direction.x, 0, direction.z);
                    m_T = 0F;
                }
                else
                {
                    m_Animator.SetTrigger("TurnRight");
                    m_ControlForward = false;
                }
            }
        }
    }

    public void Walk()
    {
        m_Animator.SetTrigger("Walk");
    }

    public void Reset()
    {
        m_Animator.SetTrigger("Stay");

        transform.position = m_OriginalPosition;

        var markerPos = m_Markers[0].position;
        var direction = markerPos - transform.position;
        m_Forward_b = new Vector3(direction.x, 0, direction.z);
        m_Forward_a = m_Forward_b;
        transform.forward = m_Forward_a;

        m_Idx = 0;
    }
}
