using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;

    private void Awake()
    {
        //Cari GameObject dengan tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Mendapatkan reference komponen
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Menuju posisi player
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else //Hentikan moving
        {
            nav.enabled = false;
        }
    }
}