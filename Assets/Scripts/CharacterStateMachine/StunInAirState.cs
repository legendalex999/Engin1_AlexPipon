using UnityEngine;

public class StunInAirState : CharacterState
{  
    public override void OnEnter()
    {  
        m_stateMachine.HitAnimation(); // passez pour le ground landing
        m_stateMachine.DizzyAnimation(true); // pour jouer l'animation additive du dizzy
        VFXManager.GetInstance().ChangeBodyColorToRed(m_stateMachine.m_hurtColorDuration);
        Debug.Log("Enter state: StunInAirState\n");
    }

    public override void OnExit()
    {
        m_stateMachine.m_IsHit = false;
        Debug.Log("Exit state: StunInAirState\n");
    }

    public override void OnFixedUpdate()
    {
       
    }

    public override void OnUpdate()
    {

    }

    public override bool CanEnter(IState currentState)
    {
        //This must be run in Update absolutely
        if (m_stateMachine.m_InAir && m_stateMachine.m_IsHit)
        {
            return true;
        }

        return false;
    }

    public override bool CanExit()
    {
        if (m_stateMachine.IsInContactWithFloor())  // pour revenir en freeState
        {
            m_stateMachine.m_InAir = false; // IMPORTANT //
            m_stateMachine.m_IsOnGround = true;
            m_stateMachine.TouchGroundAnimation(true);
            return true;
        }
        return false;




    }
}

