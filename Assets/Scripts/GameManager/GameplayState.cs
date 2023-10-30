using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayState : IState
{

    protected GameManagerSM m_gameManager;

    public GameplayState( GameManagerSM gameManager)
    {
        m_gameManager = gameManager;
    }

    public bool CanEnter(IState currentState)
    {
        return !m_gameManager.m_isCinematicState;
    }

    public bool CanExit()
    {
        return m_gameManager.m_isCinematicState;
    }

    public void OnEnter()
    {
        Debug.Log("On Enter GameplayState");
    }

    public void OnExit()
    {
        Debug.Log("On Exit GameplayState");
    }

    public void OnFixedUpdate()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            SceneManager.LoadScene("SandBox");
        }
    }
}
