﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    private int numberOfEnemies;
    [SerializeField]
    private int enemiesLeft;
    public bool isDead;

    private LoseMenu loseMenu;
    private WinMenu winMenu;
    private QuitMenu quitMenu;
    private bool activeLose = true;
    private bool activeWin = true;
    private GameSettings settings;

    public GameObject fx;
    public GameObject grassSpawner;
    public GameObject seaweedSpawner;
    public GameObject backgroundMusic;
    public GameObject cloudSpawner;
    public GameObject moonSpawner;
    public GameObject crateSpawner;
    public GameObject obstacleList;
    public List<GameObject> enemyStuffList;
    public List<GameObject> particleList;
    public GameObject water;
    public GameObject sun;

    private void Start()
    {
        settings = FindObjectOfType<GameSettings>();

        loseMenu = FindObjectOfType<LoseMenu>();
        winMenu = FindObjectOfType<WinMenu>();
        quitMenu = FindObjectOfType<QuitMenu>();

        numberOfEnemies = FindObjectsOfType<SmartEnemy>().Length;
        enemiesLeft = numberOfEnemies;

        foreach (ParticleSystem ps in FindObjectsOfType(typeof(ParticleSystem)))
        {
            ps.Stop();
        }

        CallSettings();
    }

    private void Update()
    {
        if (settings.useEnemies)
        {
            if (isDead && activeLose && activeWin)
            {
                activeLose = false;
                loseMenu.ActivateLose();
            }

            else if (getNumEnemiesLeft() <= 0 && activeWin && activeLose)
            {
                activeWin = false;
                winMenu.ActivateWin();
            }

            if (!activeLose || !activeWin)
            {
                settings.isPaused = true;
            }
        }

        if (settings.isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            ParticleLock(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            ParticleLock(false);
        }
    }

    public void DecreaseNumEnemy()
    {
        enemiesLeft--;
    }

    public int getNumEnemiesLeft()
    {
        return enemiesLeft;
    }

    private void CallSettings()
    {
        //SFX and VFX
        fx.SetActive(settings.useVFX);
        grassSpawner.SetActive(settings.useGrass);
        seaweedSpawner.SetActive(settings.useSeaweed);
        backgroundMusic.SetActive(settings.useSound);

        //Obstacles and Environment
        cloudSpawner.SetActive(settings.useClouds);
        moonSpawner.SetActive(settings.useMoons);
        crateSpawner.SetActive(settings.useCrates);
        obstacleList.SetActive(settings.useEnvironmentObjects);
        water.SetActive(settings.useWater);
        sun.SetActive(settings.useSun);

        //Enemies
        foreach (GameObject enemyThing in enemyStuffList)
        {
            enemyThing.SetActive(settings.useEnemies);
        }

        //Particles
        foreach (GameObject particle in particleList)
        {
            if (settings.useParticle)
            {
                particle.SetActive(settings.useParticle);
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                settings.SetParticleValues(ps);
            }
        }

        //UI Menu
        quitMenu.gameObject.SetActive(settings.useEnemies);
    }

    private void ParticleLock(bool active)
    {
        foreach (ParticleSystem ps in FindObjectsOfType(typeof(ParticleSystem)))
        {
            if (active)
            {
                ps.Pause();
            }
            else
            {
                ps.Play();
            }
        }
    }
}
