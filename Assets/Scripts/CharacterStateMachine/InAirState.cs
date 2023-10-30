using UnityEngine;

public class InAirState : CharacterState
{
    private Vector3 YPositionStart;
    private const float SAFE_FALLING_DISTANCE = 5f;
    private bool m_IsSafeLanding = true;
    public override void OnEnter()
    {
        m_stateMachine.m_InAir = true;
        m_IsSafeLanding = true;
        m_stateMachine.TouchGroundAnimation(false);    
        m_stateMachine.RB.drag = m_stateMachine.DragOnAir;
        YPositionStart = m_stateMachine.RB.gameObject.transform.position;
        m_stateMachine.InAirPhysic();
        Debug.Log("Enter state: InAirState\n");
     
    }

    public override void OnExit()
    {
         Debug.Log("Exit state: InAirState\n");
        m_stateMachine.DefaultPhysic();
        AudioManager.GetInstance().PlaySFX_SpatialBlend(E_SFX.Landing, 0.7f, m_stateMachine.transform.parent.transform.position, 0, 50);
    }

    public override void OnFixedUpdate()
    {
        InAirMovement();
    }

    private void InAirMovement()
    {
        Vector3 totalVector = Vector3.zero;
        int inputsNumber = 0;
        float totalSpeed = 0;

        if (Input.GetKey(KeyCode.W))
        {
            totalVector += Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
            inputsNumber++;
            totalSpeed += m_stateMachine.AccelerationValue;
        }
        if (Input.GetKey(KeyCode.A))
        {
            totalVector += Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.right, Vector3.up);
            inputsNumber++;
            totalSpeed += m_stateMachine.SideSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            totalVector += Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.forward, Vector3.up);
            inputsNumber++;
            totalSpeed += m_stateMachine.BackSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            totalVector += Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
            inputsNumber++;
            totalSpeed += m_stateMachine.SideSpeed;
        }

        float finalSpeed = 0;
        Vector3 normalizedVector = Vector3.zero;

        if (inputsNumber != 0)
        {
            finalSpeed = (totalSpeed / inputsNumber) * m_stateMachine.AccelerationAirMultiplier;
            normalizedVector = totalVector.normalized;
        }

        m_stateMachine.RB.AddForce(normalizedVector * finalSpeed, ForceMode.Acceleration);

        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxVelocity)
        {
            m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
            m_stateMachine.RB.velocity *= m_stateMachine.MaxVelocity;
        }
    }

    public override void OnUpdate()
    {
        CalculateFallingDistance();
    }

   

    public override bool CanEnter(IState currentState)
    {
        //This must be run in Update absolutely
        if (!m_stateMachine.IsInContactWithFloor() && !GameManagerSM.GetInstance().m_isCinematicState)
        {
            return true;
        }

        return false;

    }

    public override bool CanExit()
    {

        if (m_stateMachine.IsInContactWithFloor())  
        {
                     
            if (!m_IsSafeLanding)
            {               
                m_stateMachine.m_IsOnGround = true;
            }
            else
            {
                m_stateMachine.m_CanJump = false;                                                   
            }

            m_stateMachine.m_InAir = false;  // IMPORTANT //
            m_stateMachine.TouchGroundAnimation(true);
            return true;
        }

        if (m_stateMachine.m_IsHit)
        {
            return true;
        }

        return false;

    }

    private void CalculateFallingDistance()
    {
        if (m_IsSafeLanding)
        {
            float distance = YPositionStart.y - m_stateMachine.RB.gameObject.transform.position.y;

            if (distance > SAFE_FALLING_DISTANCE)
            {
                m_IsSafeLanding = false;
                m_stateMachine.HitAnimation();
            }
        }
    }
}

