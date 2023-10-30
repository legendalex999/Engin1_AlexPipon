using UnityEngine;

public class CanonBall : MonoBehaviour
{ 
    public void SetLifeTime(float lifetime)
    {
        Invoke("DestroyCanonBall", lifetime);
    }

    private void DestroyCanonBall()
    {
        Destroy(gameObject);    
    }
}
