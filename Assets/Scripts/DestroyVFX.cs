using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    [SerializeField] private float m_lifeTime;
    void Start()
    {
        Destroy(this.gameObject, m_lifeTime);
    }

  
}
