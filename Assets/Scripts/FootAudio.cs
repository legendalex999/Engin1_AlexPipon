using UnityEngine;

public class FootAudio : MonoBehaviour
{

    [SerializeField] private CharacterControllerStateMachine m_characterStateMachine;
    private bool m_isTouchGround;


    private void OnTriggerStay(Collider other)
    {
        CanonBall canonBall = other.GetComponent<CanonBall>();
        CharacterState state = m_characterStateMachine.GetCurrentState();

        if ((!m_isTouchGround && !canonBall) && state is FreeState)
        {
            m_isTouchGround = true;
            AudioManager.GetInstance().PlaySFX_SpatialBlend(E_SFX.FootStep, 0.15f , transform.position, 0, 50);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        m_isTouchGround = false;
    }
}
