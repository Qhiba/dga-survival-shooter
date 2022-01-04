using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    private Animator anim;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;

    private bool isDead;
    private bool isSinking;

    private void Awake()
    {
        //Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Set current health
        currentHealth = startingHealth;
    }

    private void Update()
    {
        //Check jika sinking
        if (isSinking)
        {
            //Memindahkan object ke bawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if (isDead)
        {
            return;
        }

        //Play audio
        enemyAudio.Play();

        //Kurangi health
        currentHealth -= amount;

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        //Set isDead
        isDead = true;

        //Set capsuleCollider ke trigger
        capsuleCollider.isTrigger = true;

        //Trigger play animation dead
        anim.SetTrigger("dead");

        //Play sound dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        //Disable navmesh component
        GetComponent<NavMeshAgent>().enabled = false;

        //Set rigidbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}