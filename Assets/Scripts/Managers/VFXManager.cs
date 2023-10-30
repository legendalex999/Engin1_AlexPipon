using Cinemachine;
using System.Collections;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject m_bloodHitPS;
    [SerializeField] private GameObject m_sphereHitPS;
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    [SerializeField] private AnimationCurve m_slowMoCurve;
    [SerializeField] private SkinnedMeshRenderer m_skinnedMeshRenderer;
    private Color m_originalColor = new Color(1, 1, 1, 1);
    private Color m_hurtColor = new Color(1, 0, 0, 1);

    private float m_currentTimer = 0;
    private float m_slowMoDuration = 0;
    private bool m_isSlowMo = false;

    private static VFXManager s_instance;

    public static VFXManager GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = FindObjectOfType<VFXManager>();
        }
        return s_instance;
    }

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
    }

    //public static VFXManager GetInstance()
    //{
    //    if( s_instance == null )
    //    {
    //        return new VFXManager();
    //    }

    //    return s_instance;
    //}

    //public VFXManager()
    //{
    //    s_instance = this;
    //}

    private void Update()
    {
        SlowMoUpdate();
    }

    /// <summary> Spawn some alien bloods drops! Tasty! </summary>
    public void BloodEffects(Vector3 contactPoint, Vector3 attackerPosition)
    {
        GameObject blood_VFX = Instantiate(m_bloodHitPS, transform.position, transform.rotation);
        blood_VFX.transform.SetParent(gameObject.transform);
        blood_VFX.transform.position = contactPoint;
        blood_VFX.transform.LookAt(attackerPosition);     
    }

    /// <summary> Spawn a hit visual effect </summary>
    public void SphereHitEffects(Vector3 contactPoint)
    {
        GameObject sphereHit_VFX = Instantiate(m_sphereHitPS, transform.position, transform.rotation);
        sphereHit_VFX.transform.SetParent(gameObject.transform);
        sphereHit_VFX.transform.position = contactPoint;    
    }

    /// <summary> Shake the camera with an intensity and a timeLimit </summary>
    public void CameraShake(float intensity, float duration)
    {
        m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        StartCoroutine(StopCameraShake(duration));
    }

    IEnumerator StopCameraShake(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }

    public void ChangeBodyColorToRed(float duration)
    {
        m_skinnedMeshRenderer.material.color = m_hurtColor;
        StartCoroutine(SetOriginalColor(duration));
    }

    IEnumerator SetOriginalColor(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        m_skinnedMeshRenderer.material.color = m_originalColor;
    }

    /// <summary> SlowMo effects based on an animation curve and a duration </summary>
    public void StartSlowMotionEffects(float duration)
    {
        if (!m_isSlowMo)
        {
            m_slowMoDuration = duration;
            m_isSlowMo = true;
        } 
    }  

    private void SlowMoUpdate()
    {
        if (m_isSlowMo)
        {
            m_currentTimer += Time.unscaledDeltaTime;            

            float evaluationPoint = m_currentTimer / m_slowMoDuration;
            Time.timeScale = m_slowMoCurve.Evaluate(evaluationPoint);

            if (m_currentTimer > m_slowMoDuration)
            {
                m_isSlowMo = false;
                m_currentTimer = 0;
                Time.timeScale = 1;
            }
        }
    }
}
