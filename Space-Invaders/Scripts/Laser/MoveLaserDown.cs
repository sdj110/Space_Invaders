using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveLaserDown : MonoBehaviour
{

    // Attributes
    float m_Speed;

    //Components
    Rigidbody2D m_RGB;

    // Get Reference to component attatched to object
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
    }

    // Initialize attributes
    void Start()
    {
        m_Speed = 250f;
    }

    void FixedUpdate()
    {
        m_RGB.velocity = Vector2.zero;
        m_RGB.AddForce(Vector2.down * m_Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
