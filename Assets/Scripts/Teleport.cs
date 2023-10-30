using System.Runtime.Serialization;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public enum Location
	{
		TopStairs,
		FallStairs,
		JumpDistanceIndicator,
		SlopeBlocks
	}

	private Vector3 m_topStairsPosition = new Vector3(425.57f, 11.95f, 472.48f);
	private Vector3 m_fallStairsPosition = new Vector3(456.76f, 6.02f, 428.59f);
	private Vector3 m_jumpDistanceIndicatorPosition = new Vector3(464.74f, 1, 447.15f);
	private Vector3 m_slopeBlocksPosition = new Vector3(480.29f, 1f, 478f);

    private Vector3 m_topStairsRotation = new Vector3(0f,270f,0f);
    private Vector3 m_fallStairsRotation = new Vector3(0f, 0f, 0f);
    private Vector3 m_jumpDistanceIndicatorRotation = new Vector3(0f, 90f, 0f);
    private Vector3 m_slopeBlocksRotation = new Vector3(0f, 90f, 0f);



    [SerializeField] private Location m_location;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCollisionDetection machine = other.gameObject.GetComponent<PlayerCollisionDetection>();

        if (machine)
        {
            Debug.Log("Teleport player");
			Vector3 teleportPosition;
            Vector3 teleportRotation;
			switch (m_location)
			{
				case Location.TopStairs:
                    teleportPosition = m_topStairsPosition;
					teleportRotation = m_topStairsRotation;
                    break;

				case Location.FallStairs:
					teleportPosition = m_fallStairsPosition;
					teleportRotation = m_fallStairsRotation;
                    break;

				case Location.JumpDistanceIndicator:
					teleportPosition = m_jumpDistanceIndicatorPosition;
					teleportRotation = m_jumpDistanceIndicatorRotation;
                    break;

				case Location.SlopeBlocks:
					teleportPosition = m_slopeBlocksPosition;
					teleportRotation = m_slopeBlocksRotation;
                    break;

				default:
                    teleportPosition = m_slopeBlocksPosition;
					teleportRotation = m_slopeBlocksRotation;
					Debug.LogError("default switch: Teleport class");
                    break;
			}
			other.gameObject.transform.position = teleportPosition;
			other.gameObject.transform.rotation = Quaternion.Euler(teleportRotation);
			other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			
            



        }
    }


}
