using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float attackDistance;
    [SerializeField] private Gun gun;

    private NavMeshAgent agent;
    private Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = PlayerMovement.Instance.transform;

        //Temporary
        target = GameObject.Find("Target").transform;
    }

    private void Update()
    {
        if (PlayerInRange())
        {
            agent.SetDestination(player.position);
            gun.Shoot();
        }
        else
            agent.SetDestination(target.position);
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
