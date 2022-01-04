using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private Animator anim;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    private bool playerInRange;
    private float timer;

    private void Awake()
    {
        //Mencari GameObject dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        //Mendapatkan komponen player health
        playerHealth = player.GetComponent<PlayerHealth>();

        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        //Mendapatkan enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }

    //Callback jika ada suatu object yang masuk kedalam trigger
    private void OnTriggerEnter(Collider other)
    {
        //Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Set player not in range
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 )
        {
            Attack();
        }

        //mentrigger animasi PlayerDead (musuh melakukan idle) jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("playerDead");
        }
    }

    private void Attack()
    {
        //Reset Timer
        timer = 0f;

        //Player takes damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}