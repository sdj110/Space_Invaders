using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Attributes
    [SerializeField] GameObject m_LaserPrefab;
    public bool IsShooter;
    public GameObject NextShooter;
    public int Score;

    // Used to initialize attributes
    void Start()
    {
        Score = 5;
        IsShooter = false;   
    }

    // Fire Laser
    public void Shoot()
    {
        Instantiate(m_LaserPrefab, transform.position, Quaternion.identity);
    }

    // Move Enemy Left
    public void MoveLeft()
    {
        transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
    }

    // Move Enemy Right
    public void MoveRight()
    {
        transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
    }

    // Move Enemy Down
    public void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
    }

    // Once removed, set next shooter to true
    public void SetNextShooter()
    {
        if (NextShooter != null)
        {
            NextShooter.GetComponent<Enemy>().IsShooter = true;
        }
    }
}
