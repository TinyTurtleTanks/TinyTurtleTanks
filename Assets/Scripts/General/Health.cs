﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour {
    public int MAXHEALTH = 3;
    [SerializeField]
    private int curHealth;
    public GameObject curHealthBar;
    private float barMax = 0.95f;
    private float barMin = 0;
    private LevelRunner levelRunner;
    public GameObject deathParticles;
    public GameSettings settings;
    private MeshRenderer[] meshRenderers;
    public Material flashMaterial;
    private List<Material> tempMaterials = new List<Material>();

    private float flashTime = 0.15f;

    void Start() {
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
        meshRenderers = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        curHealth = MAXHEALTH;
    }

    public void increaseHealth(int amount) {
        curHealth += amount;
        UpdateHealthBar();
    }

    public void decreaseHealth(int amount) {
        curHealth -= amount;
        UpdateHealthBar();
        DamageFlash();
    }

    public int getCurHealth() {
        return curHealth;
    }

    private void UpdateHealthBar() {
        float healthPercent;
        if (curHealth <= 0) {
            healthPercent = 0;
        }
        else if (curHealth < MAXHEALTH) {
            healthPercent = (float)curHealth / (float)MAXHEALTH;
        }
        else {
            healthPercent = 1;
        }

        curHealthBar.transform.localScale = (new Vector3(barMax * healthPercent + barMin, .8f, 1));
    }

    private void DamageFlash() {
        StartCoroutine("StartRotate");
    }

    IEnumerator StartRotate() {
        //save old materials and set to white
        foreach (MeshRenderer renderer in meshRenderers) {
            tempMaterials.Add(renderer.material);
            renderer.material = flashMaterial;
        }
        print("START");
        yield return new WaitForSeconds(flashTime);
        print("END");
        //reset materials to old
        for (int index = 0; index < meshRenderers.Length; index++) {
            meshRenderers[index].material = tempMaterials[index];
        }
        tempMaterials.Clear();
    }

    private void Update() {
        if (curHealth <= 0) {
            onDeath();
        }

        if (curHealth > MAXHEALTH) {
            curHealth = MAXHEALTH;
        }
    }

    protected abstract void onDeath();
}
