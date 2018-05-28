using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndByCollisionWithBottom : MonoBehaviour
{
    // Attributes
    GameController m_GameController;

    // Initialize Attributes
    void Start()
    {
        m_GameController = GameController.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            m_GameController.EndGame();
        }
    }
}
