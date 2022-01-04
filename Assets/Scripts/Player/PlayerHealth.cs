using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public Image damageImage;    
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;

    private bool isDead;
    private bool isDamaged;

    private void Awake()
    {
        //Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    private void Update()
    {
        //Jika terkena damage
        if (isDamaged)
        {
            //Merubah warna gambar menjadi valu dari flashColour
            damageImage.color = flashColor;
        }
        else
        {
            //Fade out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //Set damage to false;
        isDamaged = false;
    }

    //Fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        isDamaged = true;

        //Mengurangi health
        currentHealth -= amount;

        //Merubah tampilan dari health slider
        healthSlider.value = currentHealth;

        //Memainkan suara ketika terkena damage
        playerAudio.Play();

        //Memanggil method Death() jika darahnya kurang dari sama dengan 0 dan belum mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        //mentriger animasi Die
        anim.SetTrigger("die");

        //Memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //Mematikan script player movement
        playerMovement.enabled = false;

        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //Meload ulang scene dengan index 0 pada build setting
        SceneManager.LoadScene(0);
    }
}