using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollisionEnemy : MonoBehaviour
{
    // Attributes
    GameController m_GameController;

    // Initialize Attributes
    void Start()
    {
        m_GameController = GameController.Instance;   
    }

    // Runs when trigger hits object with a rigidbody
    void OnTriggerEnter2D(Collider2D collision)
    {
        // get Reference to GameObject we are colliding with
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            // Destroy Laser
            Destroy(gameObject);
            // TODO: DO THIS WITH GAME CONTROLLER AND REMOVE FROM LIST AND RECREATE FIRING SQUAD
            m_GameController.RemoveEnemy(obj);
        }
    }
}
