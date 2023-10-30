using UnityEngine;

public class AttackingState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.6f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        m_stateMachine.AttackingAnimation(); // Animation events
        Debug.Log("Enter state: AttackingState\n");
        AudioManager.GetInstance().PlaySFX_NoSpatialBlend(E_SFX.AirPunch, 0.9f);
    }

    public override void OnUpdate()
    {
        m_currentStateTimer += Time.deltaTime;
        if (m_currentStateTimer > STATE_EXIT_TIMER)
        {
            m_stateMachine.m_IsAttacking = false;
        }
    }

    public override void OnFixedUpdate()
    {
      
    }

    public override void OnExit()
    {
        m_stateMachine.m_IsAttacking = false;
        m_currentStateTimer = 0;
        m_stateMachine.DesactiveAttackHitbox(); //new
         Debug.Log("Exit state: AttackingState\n");
    }

    public override bool CanEnter(IState currentState)
    {       
        return m_stateMachine.m_IsAttacking;
    }

    public override bool CanExit()
    {
        if (!m_stateMachine.IsInContactWithFloor())
        {
            m_stateMachine.m_InAir = true;
            m_stateMachine.TouchGroundAnimation(false);
            return true;
        }

        if (!m_stateMachine.m_IsAttacking || (m_stateMachine.m_IsHit || m_stateMachine.m_IsOnGround))
        {
            return true;
        }     

        

        return false;
    }
}

