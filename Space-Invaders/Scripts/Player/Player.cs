using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    // Attributes
    [SerializeField] Text m_HealthText;
    [SerializeField] Text m_ScoreText;
    SpriteRenderer m_SpriteRenderer;
    bool m_Immune;
    int m_Lives;
    int m_Score;

    // Properties
    public int Score
    {
        get
        {
            return m_Score;
        }

        set
        {
            m_Score = value;
        }
    }

    // Get reference to component attatched to this object
    void Awake()
    {
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Initialize attributes
    void Start()
    {
        m_Immune = false;
        m_Lives = 3;
        m_HealthText.text = "LIVES: " + m_Lives;
        m_ScoreText.text = "SCORE: " + m_Score;
    }

    // Take Damage when hit
    public int HitByLaser()
    {
        if (!m_Immune)
        {
            m_Lives--;
            UpdateLiveText();
            StartCoroutine(FlashAlpha());
        }
        return m_Lives;
    }

    // Run when player hits enemy with laser
    public void HitEnemy(int value)
    {
        m_Score += value;
        UpdateScoreText();
    }

    // to show that you have been hit the object will flash
    public IEnumerator FlashAlpha()
    {
        Color c = m_SpriteRenderer.color;
        for (int i = 0; i < 3; i++)
        {
            m_Immune = true;
            m_SpriteRenderer.color = new Color(c.r, c.g, c.b, 0.5f);
            yield return new WaitForSeconds(0.1f);
            m_SpriteRenderer.material.color = m_SpriteRenderer.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.1f);
        }
        m_Immune = false;
    }

    // Update Lives Text
    void UpdateLiveText()
    {
        m_HealthText.text = "LIVES: " + m_Lives;
    }

    // Update the score text
    void UpdateScoreText()
    {
        m_ScoreText.text = "SCORE: " + m_Score;
    }

}
