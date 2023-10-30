using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CharacterControllerStateMachine : BaseStateMachine<CharacterState>
{

    [field: SerializeField] public CinemachineVirtualCamera Camera { get; private set; }
 
    [field: SerializeField] public Rigidbody RB { get; private set; }
    [field: SerializeField] public Animator Animator { get; set; }
    [field: SerializeField] private CapsuleCollider PlayerCollider { get; set; }

    [field: Header("Movement")]
    [field: SerializeField] public float AccelerationValue { get; private set; }
    [field: SerializeField] public float MaxVelocity { get; private set; }
    [field: SerializeField][field: Range(0.0f, 1.0f)] public float AccelerationSideMultiplier { get; private set; }
    [field: SerializeField][field: Range(0.0f, 1.0f)] public float AccelerationBackMultiplier { get; private set; }
    [field: SerializeField][field: Range(1.0f, 5.0f)] public float DragOnGround { get; private set; }

    [field: Header("Jumping")]
    [field: SerializeField] public float JumpIntensity { get; private set; } = 1000.0f;
    [field: SerializeField] public float AccelerationAirMultiplier { get; private set; }
    [field: Range(0.0f, 5.0f)][field: SerializeField] public float DragOnAir { get; private set; }
    public float SideSpeed { get; private set; }
    public float BackSpeed { get; private set; }

    public float m_hurtColorDuration = 0.5f;

    [SerializeField] private CharacterFloorTrigger m_floorTrigger;
    [SerializeField] private AnimationEventDispatcher m_animationEventDispatcher;
    [SerializeField] private PhysicMaterial m_defaultPhysicMaterial;
    [SerializeField] private PhysicMaterial m_inAirPhysicMaterial;

    [SerializeField] private Vector3 m_initialPosition;
    [SerializeField] private Vector3 m_initialRotation;

    [HideInInspector] public bool m_IsHit = false;
    [HideInInspector] public bool m_InAir = false;
    [HideInInspector] public bool m_IsOnGround = false;
    [HideInInspector] public bool m_IsGettingUp = false;
    [HideInInspector] public bool m_CanJump = true;
    [HideInInspector] public bool m_IsAttacking = false;

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new InAirState());      
        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new HitState());
        m_possibleStates.Add(new OnGroundState());
        m_possibleStates.Add(new GettingUpState());        
        m_possibleStates.Add(new StunInAirState());
        m_possibleStates.Add(new AttackingState());

        SideSpeed = AccelerationValue * AccelerationSideMultiplier;
        BackSpeed = AccelerationValue * AccelerationBackMultiplier;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerCollider.gameObject.transform.SetPositionAndRotation(m_initialPosition, Quaternion.Euler(m_initialRotation));
    }


    protected override void Start()
    {
        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

    }

    protected override void Update()
    {        
        base.Update();
    }

    protected override void FixedUpdate()
    {     
        if (GameManagerSM.GetInstance().m_isCinematicState)
            return;

        base.FixedUpdate();        
    }

    public CharacterState GetCurrentState()
    {
        return m_currentState;
    }

    public bool IsInContactWithFloor()
    {
        return m_floorTrigger.IsOnFloor;
    }

    public void UpdateAnimationValue(Vector3 movementVecValue)
    {
        Animator.SetFloat("MoveX", movementVecValue.x);
        Animator.SetFloat("MoveY", movementVecValue.y);
    }

    public void HitAnimation()
    {
        Animator.SetTrigger("Hit");
    }

    public void JumpAnimation()
    {
        Animator.SetTrigger("Jump");
    }

    public void AttackingAnimation()
    {
        Animator.SetTrigger("Attacking");
    }

    public void GettingUpAnimation()
    {
        Animator.SetTrigger("GettingUp");
    }

    public void DizzyAnimation(bool value)
    {
        Animator.SetBool("Dizzy", value);
    }

    public void TouchGroundAnimation(bool value)
    {
        Animator.SetBool("TouchGround", value);
    }

    public void HitResetAnimation()
    {
        Animator.ResetTrigger("Hit");
    }

    public void DesactiveAttackHitbox() 
    {
        m_animationEventDispatcher.DeactivateAttackHitbox();
    }

    public void ActiveAttackHitbox()
    {
        m_animationEventDispatcher.ActivateAttackHitbox();
    }

    public void InAirPhysic()
    {
        PlayerCollider.material = m_inAirPhysicMaterial;
    }

    public void DefaultPhysic()
    {
        PlayerCollider.material = m_defaultPhysicMaterial;

    }

}
