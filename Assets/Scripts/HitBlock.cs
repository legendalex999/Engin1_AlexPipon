using UnityEngine;

public class HitBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerCollisionDetection machine = collision.gameObject.GetComponent<PlayerCollisionDetection>();

        if (machine)
        {          
            CharacterState state = machine.m_stateMachine.GetCurrentState();
            ContactPoint contact = collision.contacts[0];
            Vector3 contactPoint = contact.point;

            if (state is FreeState)
            {
                SetHitToPlayer(machine, contactPoint);
            }
            else if (state is InAirState) 
            {
                SetHitToPlayer(machine, contactPoint);
            }
           
        }
    }

    private static void SetHitToPlayer(PlayerCollisionDetection machine, Vector3 position)
    {
        if (!machine.m_stateMachine.m_IsHit)
        {       
            AudioManager.GetInstance().PlaySFX_SpatialBlend(E_SFX.Hit, 1 , position, 0, 50);
            VFXManager.GetInstance().CameraShake(10, 0.25f);
            machine.m_stateMachine.m_IsHit = true;
            Debug.Log("Player is Hit");
        }
    }



   
}
