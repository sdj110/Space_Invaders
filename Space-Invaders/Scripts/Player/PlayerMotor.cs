using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    // Attributes
    Vector2 m_DeltaForce;
    float m_Speed;

    // Components
    Rigidbody2D m_RGB;

    // Used to get reference to component attatched to object
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();    
    }

    // Used to Initailize
    void Start()
    {
        m_Speed = 75;
        m_DeltaForce = Vector2.zero;
    }

    // Used to manipulate physics components 
    void FixedUpdate()
    {
        // Check Keyboard for movement input   
        GetMovementInput();

        // Apply Movement force to rigidbody
        ApplyMovementForce();
    }

    // Check for player movement input
    void GetMovementInput()
    {
        // Check preset horizontal movement key for movement force direction
        float x = Input.GetAxisRaw("Horizontal");

        // Create force out of movement input
        m_DeltaForce = new Vector2(x, 0);
    }

    // Use rigidbody to move object based on movement vector
    void ApplyMovementForce()
    {
        // Reset the rigidbody's velocity vector each frame
        m_RGB.velocity = Vector2.zero;

        // Grab old position
        Vector3 pos = transform.position;

        // Apply new force
        m_RGB.AddForce(m_DeltaForce * m_Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

}

