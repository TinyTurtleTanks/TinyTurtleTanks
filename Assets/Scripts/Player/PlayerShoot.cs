﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    private Transform parent;

    private void Start() {
        parent = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            Instantiate(bullet, shootPoint.position, this.transform.rotation, parent);
        }
    }
}