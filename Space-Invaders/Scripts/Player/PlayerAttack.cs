using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attributes
    [SerializeField] GameObject m_LaserPrefab;
    Player m_PlayerScript;
    float m_FireRate;
    float m_AttackDelay;

    // Get reference to component
    void Awake()
    {
        m_PlayerScript = GetComponent<Player>();
    }

    // Used to Initialize attributes
    void Start()
    {
        m_FireRate = 1.5f;   
    }

    // Runs once a frame
    void Update()
    {
        CheckForAttack();   
    }

    // Check for mouse input to attack
    void CheckForAttack()
    {
        // Check for controller input
        if (m_FireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > m_AttackDelay)
            {
                m_AttackDelay = Time.time + (1 / m_FireRate);
                Shoot();
            }
        }
    }

    // Check if attack input is true and then create laser prefab
    void Shoot()
    {
        Instantiate(m_LaserPrefab, transform.position, Quaternion.identity);
    }

    // Runs when you hit the enemy player
    public void HitEnemy(int value)
    {
        m_PlayerScript.HitEnemy(value);
    }
}
