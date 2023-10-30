using UnityEngine;

public class CinematicState : IState
{
    protected GameManagerSM m_gameManager;

    public CinematicState(GameManagerSM gameManager)
    {
        m_gameManager = gameManager;
    }

    public bool CanEnter(IState currentState)
    {
        return m_gameManager.m_isCinematicState;
    }

    public bool CanExit()
    {
        return !m_gameManager.m_isCinematicState;
    }

    public void OnEnter()
    {
        Debug.Log("On Enter CinematicState");
        m_gameManager.m_characterController.UpdateAnimationValue(Vector3.zero);
    }

    public void OnExit()
    {
        Debug.Log("On Exit CinematicState");
    }

    public void OnFixedUpdate()
    {
    }

    public void OnStart()
    {
    }

    public void OnUpdate()
    {
    }
}
