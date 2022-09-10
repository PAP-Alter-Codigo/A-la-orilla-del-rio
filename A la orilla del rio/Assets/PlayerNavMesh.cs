using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    public Transform objetivo;
    private NavMeshAgent agente;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }


    void Update()
    {
        agente.SetDestination(objetivo.position);
    }
}
