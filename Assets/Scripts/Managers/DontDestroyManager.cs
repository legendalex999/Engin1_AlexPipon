using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
  [SerializeField] private List<GameObject> m_dontDestroyObjects = new List<GameObject>();

    private static DontDestroyManager s_instance;

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;         
        }
        else
        {
            DestroyAllDuplicate();
        }
    }
    private void Start()
    {
        foreach (GameObject obj in m_dontDestroyObjects)
        {
            DontDestroyOnLoad(obj);
        }
    }

    private void DestroyAllDuplicate()
    {
        foreach (GameObject obj in m_dontDestroyObjects)
        {
            if (obj)
            {
                Destroy(obj);
            }
         
        }

        Destroy(gameObject);
    }


}
