using UnityEngine;

public enum E_SFX
{
    Jump,
    Landing,
    Punch,
    AirPunch,
    FootStep,
    Hit,
    CanonShot,
    SlamGround
}
public class AudioManager : MonoBehaviour
{
    private static AudioManager s_instance;

    public static AudioManager GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = FindObjectOfType<AudioManager>();
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

    /// <summary> Play a temporary clip with a volume </summary>
    public void PlaySFX_NoSpatialBlend(E_SFX effect, float volume)
    {    
      AudioClip effectClip = Resources.Load<AudioClip>("Audio/" + effect.ToString());              
      AudioSource effectSource = gameObject.AddComponent<AudioSource>();

      effectSource.clip = effectClip;
      effectSource.volume = volume;
      effectSource.Play();

      Destroy(effectSource, effectClip.length);
    }

    /// <summary> Play a temporary clip at a position, a volume between 0 and 1, choose min at max distance of the noise </summary>
    public void PlaySFX_SpatialBlend(E_SFX clipEnum, float volume, Vector3 newPosition, float minDistance, float maxDistance)
    {
        AudioClip newClip;

        if (clipEnum == E_SFX.Punch)
        {
            newClip = Resources.Load<AudioClip>("Audio/Punch/" + RandomPunchSound().ToString());
        }
        else
        {
            newClip = Resources.Load<AudioClip>("Audio/" + clipEnum.ToString());
        }

        GameObject audioGO = new GameObject();
        AudioSource effectSource = audioGO.AddComponent<AudioSource>();

        audioGO.transform.position = newPosition;
        audioGO.transform.SetParent(transform); 
        audioGO.name = newClip.name + " Audio GO";

        effectSource.clip = newClip;
        effectSource.spatialBlend = 1;
        effectSource.rolloffMode = AudioRolloffMode.Linear;
        effectSource.maxDistance = maxDistance;
        effectSource.minDistance = minDistance;
        effectSource.volume = volume;
        effectSource.Play();

        Destroy(audioGO, newClip.length);
    }


    private int RandomPunchSound()
    {
        return Random.Range(0, 17);
    }
}
