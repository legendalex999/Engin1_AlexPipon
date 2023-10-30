using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.5f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        m_stateMachine.m_InAir = true;
        m_stateMachine.TouchGroundAnimation(false);
        m_stateMachine.RB.drag = m_stateMachine.DragOnAir;
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.JumpAnimation();
        AudioManager.GetInstance().PlaySFX_SpatialBlend(E_SFX.Jump, 0.7f, m_stateMachine.transform.parent.transform.position, 0, 50);
        Debug.Log("Enter state: JumpState\n");

    }

    public override void OnExit()
    {
        m_currentStateTimer = 0;
 
        Debug.Log("Exit state: JumpState\n");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(IState currentState)
    {
        //This must be run in Update absolutely
        if ((!m_stateMachine.m_InAir && m_stateMachine.m_CanJump) && !GameManagerSM.GetInstance().m_isCinematicState)
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        return false;
 


    }

    public override bool CanExit()
    {
        return true;
    }
}
