﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

    float shieldTimer = 0.0f;
    public GameObject shieldPrefab;
    private GameObject shield;

    float invisibilityTimer = 0.0f;

    private float health;
    public float fullHealth = 100.0f;
    private int lives = 3;

    float score;
    float gameDuration;

    private int originalLayer;

    private PlayerMovement movement;
    // Use this for initialization
    void Start() {
        health = fullHealth;
        originalLayer = gameObject.layer;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (lives < 1) {
            return;
        }

        if (shieldTimer > 0.0f) {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer < 0.0f) {
                Destroy(shield);
            }
        }

        if (invisibilityTimer > 0.0f) {
            invisibilityTimer -= Time.deltaTime;

            if (invisibilityTimer < 0.0f) {
                gameObject.layer = originalLayer;

                if (shieldTimer > 0.0f) {
                    shield.layer = originalLayer;
                }
            }
        }
    }

    public void SpeedBoost(float amount, float time) {
        movement.Speedbuff(amount, time);
    }

    //Slowdown
    public void SpeedBuff(float amount, float time) {
        movement.SlowDown(amount, time);
    }

    public void Invisibility(float time) {
        invisibilityTimer = time;
        gameObject.layer = 9; // Only PC layer
    }

    public void Shield(float time) {
        shieldTimer = time;
        shield = Instantiate(shieldPrefab, transform);
        if (invisibilityTimer > 0.0f) {
            //shield.GetComponent<Renderer>().enabled = false;
            shield.layer = 9; // Only PC layer
        }
    }

    public void Kill() {
        Debug.Log("You died!");
        lives--;
        Respawn(active:(lives > 0));
    }

    public void Damage(float amount) {
        health -= amount;

        if (health < 0.0f) {
            Kill();
        }
    }

    private void Respawn(bool active) {
        Debug.Log(active);

        health = fullHealth;

        ExtendedNetworkManager nm = (ExtendedNetworkManager) NetworkManager.singleton;
        transform.position = nm.PCPlayerSpawns.getPlayerSpawn();

        if (!active) {
            GetComponent<PlayerMovement>().enabled = false;
            // TODO Fail message
        }
    }

    public void OnGameStarted(int lives, float gameDuration) {
        this.lives = lives;
        this.gameDuration = gameDuration;
    }

    public void OnGameFinished(PlayerStats[] allStats) {
        // TODO Display scores
    }
}
