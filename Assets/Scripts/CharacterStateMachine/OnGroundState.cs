using UnityEngine;
using UnityEngine.UIElements;

public class OnGroundState : CharacterState
{
    private const float HIT_DURATION = 0.767f;
    private float m_currentTime = 0;
    private bool m_GetUp = false;
    public override void OnEnter()
    {
        m_stateMachine.DizzyAnimation(false);
        m_stateMachine.HitResetAnimation(); // Bug fix
        m_stateMachine.m_IsOnGround = false;
        m_stateMachine.m_IsGettingUp = true;
        AudioManager.GetInstance().PlaySFX_SpatialBlend(E_SFX.Hit, 1, m_stateMachine.transform.position, 0, 50);
        Debug.Log("Enter state: OnGroundState\n");
    }

    public override void OnUpdate()
    {
        m_currentTime += Time.deltaTime;
        bool AnyMoveKeyPressed = (Input.GetKey(KeyCode.W) 
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D));

        if (AnyMoveKeyPressed && m_currentTime >= HIT_DURATION)
        {
            m_GetUp = true;        
        
          //  Debug.Log("Get up true");
        }
        
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {      
        m_GetUp = false;
        m_currentTime = 0;
        Debug.Log("Exit state: OnGroundState\n");
    }

    public override bool CanEnter(IState currentState)
    {
        //Je ne peux entrer dans le Onground State que si le bool est vrai
        return m_stateMachine.m_IsOnGround;
    }

    public override bool CanExit()
    {
        if (m_GetUp)
        {           
            return true;
        }
        return false;
    }
}
