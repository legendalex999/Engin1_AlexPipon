using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator m_animator;
    private bool m_isHit = false;
    private float m_currentTime = 0;
    private const float HIT_DELAY = 0.65f;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public bool GetIsHit()
    {
        return m_isHit;
    }

    private void Update()
    {
        if (m_isHit)
        { 
            m_currentTime += Time.deltaTime;

            if (m_currentTime > HIT_DELAY)
            {
                m_isHit = false;
                m_currentTime = 0;
            }
        }
    }

    public void TakeDamage()
    {
        if (!m_isHit)
        {
            m_isHit = true;
            m_animator.SetTrigger("Hit");   
        }
  
      
    }
}
