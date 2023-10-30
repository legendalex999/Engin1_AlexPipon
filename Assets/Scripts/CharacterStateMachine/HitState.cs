using UnityEngine;

public class HitState : CharacterState
{
    private const float HIT_DURATION = 0.533f;
    private float m_currentTime = 0;

    public override void OnEnter()
    {

        Debug.Log("Enter state: HitState\n");
        m_stateMachine.HitAnimation();
        VFXManager.GetInstance().ChangeBodyColorToRed(m_stateMachine.m_hurtColorDuration);
     
    }

    public override void OnUpdate()
    {
        m_currentTime += Time.deltaTime;
    }

    public override void OnFixedUpdate()
    {


    }

    public override void OnExit()
    {
        m_currentTime = 0;
        m_stateMachine.m_IsHit = false;

       

        Debug.Log("Exit state: HitState\n");
        
    }

    public override bool CanEnter(IState currentState)
    {       
        return ((m_stateMachine.m_IsHit && !m_stateMachine.m_InAir) && !m_stateMachine.m_IsOnGround);
    }

    public override bool CanExit()
    {
        if (m_currentTime >= HIT_DURATION)
        {
            return true;
        }

        return false;


    }
}
