using UnityEngine;

public class GettingUpState : CharacterState
{
    private const float HIT_DURATION = 0.867f;
    private float m_currentTime = 0;
    public override void OnEnter()
    {
        m_stateMachine.GettingUpAnimation();
        Debug.Log("Enter state: GettingUpState\n");

    }

    public override void OnUpdate()
    {
        m_currentTime += Time.deltaTime;
        if (m_currentTime > HIT_DURATION)
        {
            m_stateMachine.m_IsGettingUp = false;
        }

    }

    public override void OnFixedUpdate()
    {


    }

    public override void OnExit()
    {
        m_currentTime = 0;
        Debug.Log("Exit state: GettingUpState\n");
    }

    public override bool CanEnter(IState currentState)
    {
        //Je ne peux entrer dans le Onground State que si le bool est vrai
        return m_stateMachine.m_IsGettingUp;

    }

    public override bool CanExit()
    {
        if (!m_stateMachine.m_IsGettingUp)
        {
            return true;
        }

        return false;


    }
}
