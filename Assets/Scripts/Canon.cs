using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private float m_delayFireInSeconds;
    [SerializeField] private float m_shootForce;
    [SerializeField] private float m_canonBallLifetimeInSeconds;
    [SerializeField] private CanonBall m_canonBallPrefab;
    [SerializeField] private GameObject m_canonBallStart;

    private const int INVOKE_START_TIME = 1;
    void Start()
    {
        InvokeRepeating("FireCanonBall", INVOKE_START_TIME, m_delayFireInSeconds);       
    }

    private void FireCanonBall()
    {
        Vector3 launchDirection = (m_canonBallStart.transform.position - transform.position).normalized;

        CanonBall canonBall = Instantiate(m_canonBallPrefab, m_canonBallStart.transform.position, Quaternion.identity);
        canonBall.transform.SetParent(transform.parent.transform);
        canonBall.SetLifeTime(m_canonBallLifetimeInSeconds);
  
        canonBall.GetComponent<Rigidbody>().AddForce(launchDirection * m_shootForce, ForceMode.Impulse);
    }
 
}
