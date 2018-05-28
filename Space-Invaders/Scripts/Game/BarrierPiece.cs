using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPiece : MonoBehaviour
{
    // Attributes
    int m_TimesHit;

    // Use this for initialization
    void Start()
    {
        m_TimesHit = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Laser"))
        {
            Destroy(obj);
            m_TimesHit++;
        }

        if (m_TimesHit >= 3)
        {
            Destroy(this.gameObject);
        }
        print(m_TimesHit);
    }
}
