using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    ActorManager m_ActorManager;
    public string Tag { get; set; }
    void Start()
    {
        m_ActorManager = GameObject.FindObjectOfType<ActorManager>();
        
        if(!m_ActorManager.Actors.Contains(this))
        {
            m_ActorManager.Actors.Add(this);
        }
    }

    private void OnDestroy()
    {
        if (m_ActorManager)
        {
            m_ActorManager.Actors.Remove(this);
        }
    }
}
