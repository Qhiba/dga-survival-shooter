﻿using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [SerializeField]
    private MonoBehaviour factory;
    private IFactory Factory { get { return factory as IFactory; } }

    private void Start()
    {
        //Mengeksekusi fungsi Spawn setiap beberapa detik sesuai dengan nilai spawnTime
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        //Jika player telah mati maka tidak akan membuat enemy baru
        if (playerHealth.currentHealth < 0)
        {
            return;
        }

        //Mendapat nilai random
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnEnemy = Random.Range(0, 3);

        //Menduplikasi enemy
        Factory.FactoryMethod(spawnEnemy);
    }
}