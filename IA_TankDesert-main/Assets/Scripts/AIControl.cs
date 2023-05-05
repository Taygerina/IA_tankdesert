using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public NavMeshAgent agent;

    void Start()
    {
        // Pegar o componente NavMeshAgent para passar para o Manager
        agent = this.GetComponent<NavMeshAgent>();
    }
}
