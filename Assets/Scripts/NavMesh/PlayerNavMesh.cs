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
        //Se deshabilitan funciones de navmesh3D, para que funcione bien en 2D
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }


    void Update()
    {
        //Se establece el destino del navmesh
        agente.SetDestination(objetivo.position);
    }
}
