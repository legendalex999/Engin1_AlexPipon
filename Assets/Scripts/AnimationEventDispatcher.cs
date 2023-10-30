using UnityEngine;

public class AnimationEventDispatcher : MonoBehaviour
{
    [SerializeField] private SphereCollider m_attackHitbox;

    public void ActivateAttackHitbox()
    {
        m_attackHitbox.gameObject.SetActive(true);       
    }

    public void DeactivateAttackHitbox()
    {
        m_attackHitbox.gameObject.SetActive(false);      
    }
}
