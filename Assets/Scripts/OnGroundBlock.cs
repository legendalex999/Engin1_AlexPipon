using UnityEngine;

public class OnGroundBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerCollisionDetection machine = collision.gameObject.GetComponent<PlayerCollisionDetection>();

        if (machine)
        {
            CharacterState state = machine.m_stateMachine.GetCurrentState();

            if (state is FreeState)
            {
                SetOnGroundToPlayer(machine);
            }
            else if (state is AttackingState)
            {
                SetOnGroundToPlayer(machine);
            }
            else if (state is InAirState)
            {
                SetOnGroundToPlayer(machine);
            }
        }
    }

    private static void SetOnGroundToPlayer(PlayerCollisionDetection machine)
    {
        if (!machine.m_stateMachine.m_IsOnGround)
        {
            machine.m_stateMachine.m_IsOnGround = true;
            machine.m_stateMachine.Animator.SetTrigger("KO");
            Debug.Log("Player is on ground");
        }
    }


}
