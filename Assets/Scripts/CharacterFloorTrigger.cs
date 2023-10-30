using UnityEngine;

public class CharacterFloorTrigger : MonoBehaviour
{
    public bool IsOnFloor { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        CanonBall canonBall = other.GetComponent<CanonBall>();

        if (!IsOnFloor && !canonBall )
        {
            Debug.Log("Vient de toucher le sol");
            IsOnFloor = true;
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Vient de quitter le sol");
        IsOnFloor = false;
    }
}
