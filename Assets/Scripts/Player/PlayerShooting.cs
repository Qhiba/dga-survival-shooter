using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    private float timer;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectDisplayTime = 0.2f;

    private void Awake()
    {
        //Get mask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        //disable line renderer
        gunLine.enabled = false;

        //diable light
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;

        //Play audio
        gunAudio.Play();

        //Enable light
        gunLight.enabled = true;

        //Play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        //enable Line renderer dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        //Set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Lakukan raycast jika mendeteksi id enemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit component EnemyHealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                //Lakukan take damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            //Set line end position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            //Set line end position ke range from barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}