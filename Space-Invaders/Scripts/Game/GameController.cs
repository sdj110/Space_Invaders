using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Enum to control Game states
    enum GAME_STATE
    {
        RUNNING,
        END,
    }

    // Attributes
    [SerializeField] GameObject m_MenuManager;
    [SerializeField] GameObject m_EnemyPrefab;
    [SerializeField] GameObject m_Player;
    GAME_STATE m_State;
    int m_Width = 11;
    int m_Height = 5;
    List<GameObject> m_EnemyList;
    List<GameObject> m_ShooterList;
    float m_EnemyMovementDelay;
    int m_TotalNumberOfEnemies;
    bool m_MoveRight;
    float m_AttackDelay;
    int m_MoveCounter;
    int m_MaxMoveCounter;

    #region Singleton
    public static GameController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Used to initialize
    void Start()
    {
        m_State = GAME_STATE.RUNNING;
        m_EnemyMovementDelay = 1;
        m_EnemyList = new List<GameObject>();
        m_ShooterList = new List<GameObject>();
        m_MoveRight = true;
        m_MoveCounter = 0;
        m_MaxMoveCounter = 6;
        m_AttackDelay = 3f;
        m_TotalNumberOfEnemies = m_Width * m_Height;
        CreateEnemies();
        ResetShooterList();
        MoveInvaders();

        Invoke("EnemyAttack", Random.Range(0, m_AttackDelay));
    }

    // Instantiate enemies in 2D grid pattern
    void CreateEnemies()
    {
        // Define starting location for reference 
        float startX = -4.5f;
        float startY = 4;

        // Loop through 2D grid 
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                // Create new position 
                Vector3 pos = new Vector3(startX + (0.6f * x), startY + (0.6f * y));

                // Create new Enemy:GameObject
                GameObject clone = Instantiate(m_EnemyPrefab, pos, Quaternion.identity);

                // If on bottom then set as shooter
                if (y == 0)
                {
                    clone.GetComponent<Enemy>().IsShooter = true;
                }
                // Add to enemy list
                m_EnemyList.Add(clone);
            }
        }
        // Link Each Shooter
        CreateShooterList();
    }

    // Create Shooter Linked List
    void CreateShooterList()
    {
        // Loop through 2D grid 
        int index = 0;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                if (index < m_EnemyList.Count - m_Width)
                {
                    m_EnemyList[index].GetComponent<Enemy>().NextShooter = m_EnemyList[index + m_Width];
                }
                else
                {
                    m_EnemyList[index].GetComponent<Enemy>().NextShooter = null;
                }
                index++;
            }
        }
    }

    // Once enemy is removed check if activate new shooter 
    void ResetShooterList()
    {
        m_ShooterList = new List<GameObject>();
        foreach (GameObject enemy in m_EnemyList)
        {
            if (enemy.GetComponent<Enemy>().IsShooter)
            {
                m_ShooterList.Add(enemy);
            }
        }
    }

    // Runs when player is hit by enemy laser
    public void HitPlayer(GameObject obj)
    {
        if (obj.GetComponent<Player>().HitByLaser() <= 0)
        {
            EndGame();
        }
    }

    // Remove enemy when hit with player laser
    public void RemoveEnemy(GameObject obj)
    {
        // Get index of object and remove from lists
        int index = m_EnemyList.IndexOf(obj);
        m_EnemyList.Remove(obj);

        // Award player score
        m_Player.GetComponent<PlayerAttack>().HitEnemy(obj.GetComponent<Enemy>().Score);
        
        // Reset shooter status
        obj.GetComponent<Enemy>().IsShooter = false;

        // Set next shooter
        obj.GetComponent<Enemy>().SetNextShooter();

        // Create new shooter List
        ResetShooterList();

        // Remove Object from the game
        Destroy(obj);

        // Speed of enemy is based on number of enemies left
        IncreaseEnemyMovementSpeed();
    }

    // Increase enemy movemeny speed based on number they have left
    void IncreaseEnemyMovementSpeed()
    {
        if (m_EnemyList.Count == 1)
        {
            m_EnemyMovementDelay = 0.1f;
        }
        else if (m_EnemyList.Count == 2)
        {
            m_EnemyMovementDelay = 0.2f;
        }
        else if (m_EnemyList.Count <= 5)
        {
            m_EnemyMovementDelay = 0.5f;
        }
        else if (m_EnemyList.Count <= 10)
        {
            m_EnemyMovementDelay = 0.6f;
        }
        else if (m_EnemyList.Count <= m_TotalNumberOfEnemies / 4)
        {
            m_EnemyMovementDelay = 0.6f;
        }
        else if (m_EnemyList.Count <= m_TotalNumberOfEnemies / 3)
        {
            m_EnemyMovementDelay = 0.6f;
        }
        else if (m_EnemyList.Count <= m_TotalNumberOfEnemies / 2)
        {
            m_EnemyMovementDelay = 0.8f;
        }
        else
        {

        }
    }

    // Randomly select shooters in list to fire laser at player
    void EnemyAttack()
    {
        if (m_State == GAME_STATE.RUNNING)
        {
            int random_index = Random.Range(0, m_ShooterList.Count - 1);
            m_ShooterList[random_index].GetComponent<Enemy>().Shoot();
        }
        Invoke("EnemyAttack", Random.Range(0, m_AttackDelay));
    }

    // Various ways to change game state to end game
    public void EndGame()
    {
        m_State = GAME_STATE.END;
        m_MenuManager.GetComponent<MenuManager>().LoadEndGame();
    }

    #region Move Invaders
    // Move the Invaders as a group
    void MoveInvaders()
    {
        if (m_State == GAME_STATE.RUNNING)
        {
            if (m_MoveRight)
            {
                if (m_MoveCounter == 0)
                {
                    MoveInvadersRight();
                    m_MoveCounter++;
                }
                else if (m_MoveCounter == m_MaxMoveCounter)
                {
                    MoveInvadersDown();
                    m_MoveRight = false;
                }
                else
                {
                    MoveInvadersRight();
                    m_MoveCounter++;
                }
            }
            else
            {
                if (m_MoveCounter == 0)
                {
                    MoveInvadersDown();
                    m_MoveRight = true;
                }
                else if (m_MoveCounter == m_MaxMoveCounter)
                {
                    MoveInvadersLeft();
                    m_MoveCounter--;
                }
                else
                {
                    MoveInvadersLeft();
                    m_MoveCounter--;
                }
            }
        }
        Invoke("MoveInvaders", m_EnemyMovementDelay);
    }

    // Move all Enemies left
    void MoveInvadersLeft()
    {
        foreach (GameObject invader in m_EnemyList)
        {
            if (invader != null)
            {
                invader.GetComponent<Enemy>().MoveLeft();
            }
        }
    }

    // Move all Enemies Right
    void MoveInvadersRight()
    {
        foreach (GameObject invader in m_EnemyList)
        {
            if (invader != null)
            {
                invader.GetComponent<Enemy>().MoveRight();
            }
        }
    }
    // Move all Enemies downs
    void MoveInvadersDown()
    {
        foreach (GameObject invader in m_EnemyList)
        {
            if (invader != null)
            {
                invader.GetComponent<Enemy>().MoveDown();
            }
        }
    }
    #endregion

}
