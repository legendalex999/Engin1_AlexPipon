using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum Objects
    {
        Hand,
        Enemy,
        Allies,
        Neutral
    }

    [SerializeField] private Objects m_Object;
    [SerializeField] private bool m_CanHit;
    [SerializeField] private bool m_CanBeHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (m_CanHit)
        {
            Hitbox otherHitBox = collision.gameObject.GetComponent<Hitbox>();

            if (otherHitBox != null)
            {                          
                if (otherHitBox.m_Object == Objects.Enemy && m_Object == Objects.Hand)
                {
                    HitEnemy(collision, otherHitBox);
                    return;
                }
                // Others situations Below...
            }
        }
    }

    public void OnTrigger(Objects other)
    {
        if (m_CanBeHit)
        {
            if (m_Object == Objects.Enemy && other == Objects.Hand)
            {
                GetComponent<EnemyController>().TakeDamage();
            }
            // Others situations Below...
        }
    }


    private void HitEnemy(Collision collision, Hitbox otherHitBox)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (!enemy.GetIsHit())
        {
            otherHitBox.OnTrigger(Objects.Hand);
            Debug.Log(gameObject.name + " has it " + collision.gameObject.name);

            ContactPoint contact = collision.contacts[0];
            Vector3 contactPoint = contact.point;
            VFXManager instanceVFX = VFXManager.GetInstance();
            instanceVFX.SphereHitEffects(contactPoint);
            instanceVFX.BloodEffects(contactPoint, transform.position);
            instanceVFX.CameraShake(2, 0.75f);
            instanceVFX.StartSlowMotionEffects(1.25f);
            AudioManager.GetInstance().PlaySFX_SpatialBlend(E_SFX.Punch, 1, contactPoint, 0, 50);
        }
    }
}