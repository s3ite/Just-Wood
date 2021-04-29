using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrollAi : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    private NavMeshAgent _navMeshAgent;
    
    //private bool playerInAttackRange;

    //private GameObject player;

    //private float attackRange;
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    private void ChasePlayer()
    {
        if (_destination)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }
    

    void OnTriggerEnter(Collider col){

        //Debug.Log("Collision Faite");
        if(col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Un elfe est entrer pour se faire tuer");
            //Destroy(col.gameObject);
        }
    }
}
