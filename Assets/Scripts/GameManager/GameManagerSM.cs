using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Playables;

public class GameManagerSM : BaseStateMachine<IState>
{
    public CharacterControllerStateMachine m_characterController;

    [SerializeField] private CameraController m_cameraController;

    [HideInInspector] public bool m_isCinematicState = true;

    [SerializeField] private PlayableDirector m_playableDirector;
    [SerializeField] private float m_initialTime;

    [SerializeField] private GameObject m_mainCharacter;


    private static GameManagerSM s_instance;

    public static GameManagerSM GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = FindObjectOfType<GameManagerSM>();
        }
        return s_instance;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded: " + scene.name);

        if (!m_isCinematicState)
        {
            ChangeState();
        }

        m_playableDirector.initialTime = m_initialTime;
        m_playableDirector.Play();

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void Awake()
    {
        base.Awake();

        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new CinematicState(this));
        m_possibleStates.Add(new GameplayState(this));
    }

    public void ChangeState()
    {
        if (m_isCinematicState)
        {
            m_isCinematicState = false;
            m_cameraController.enabled = true;

        }
        else
        {
            m_isCinematicState = true;
            m_cameraController.enabled = false;
        }
    }
}