using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour {

    float shieldTimer = 0.0f;
    public GameObject shieldPrefab;
    private GameObject shield;

    float invisibilityTimer = 0.0f;
    float speedboostTimer = 0.0f;
    float slowdownTimer = 0.0f;

    private float health;
    public float fullHealth = 100.0f;
    private int lives = 3;

    private GameObject[] livesUI = new GameObject[3];
    private GameObject invisibilityUI;
    private GameObject speedboostUI;
    private GameObject shieldUI;
    private GameObject slowdownUI;

    float score;
    float gameDuration;

    private int originalLayer;

    private PlayerMovement movement;
    // Use this for initialization
    void Start() {
        health = fullHealth;
        originalLayer = gameObject.layer;
        movement = GetComponent<PlayerMovement>();

        Canvas can = GetComponentInChildren<Canvas>();
        livesUI[0] = can.transform.Find("Live1").gameObject;
        livesUI[1] = can.transform.Find("Live2").gameObject;
        livesUI[2] = can.transform.Find("Live0").gameObject;
        invisibilityUI = can.transform.Find("Invisibility").gameObject;
        speedboostUI = can.transform.Find("SpeedBoost").gameObject;
        shieldUI = can.transform.Find("Shield").gameObject;

        slowdownUI = can.transform.Find("SlowDown").gameObject;

        invisibilityUI.SetActive(false);
        speedboostUI.SetActive(false);
        shieldUI.SetActive(false);
        slowdownUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (lives < 1) {
            return;
        }

        if (shieldTimer > 0.0f) {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer < 0.0f) {
                shieldUI.SetActive(false);
                Destroy(shield);
            }
        }

        if (invisibilityTimer > 0.0f) {
            invisibilityTimer -= Time.deltaTime;

            if (invisibilityTimer < 0.0f) {
                invisibilityUI.SetActive(false);
                CmdResetInvisibility();
            }
        }

        if (speedboostTimer > 0.0f) {
            speedboostTimer -= Time.deltaTime;

            if (speedboostTimer < 0.0f) {
                speedboostUI.SetActive(false);
            }
        }

        if (slowdownTimer > 0.0f) {
            slowdownTimer -= Time.deltaTime;

            if (slowdownTimer < 0.0f) {
                slowdownUI.SetActive(false);
            }
        }
    }

    public void SpeedBoost(float amount, float time) {
        speedboostTimer = time;
        movement.Speedbuff(amount, time);
        speedboostUI.SetActive(true);
    }

    //Slowdown
    public void SpeedBuff(float amount, float time) {
        slowdownTimer = time;
        movement.SlowDown(amount, time);
        slowdownUI.SetActive(true);
    }

    [Command]
    public void CmdResetInvisibility() {
        gameObject.layer = originalLayer;

        if (shieldTimer > 0.0f) {
            shield.layer = originalLayer;
        }
    }

    [Command]
    public void CmdInvisibility(float time) {
        invisibilityTimer = time;
        gameObject.layer = 9; // Only PC layer

        invisibilityUI.SetActive(true);
    }

    public void Shield(float time) {
        shieldTimer = time;
        shield = Instantiate(shieldPrefab, transform);
        if (invisibilityTimer > 0.0f) {
            //shield.GetComponent<Renderer>().enabled = false;
            shield.layer = 9; // Only PC layer
        }
        shieldUI.SetActive(true);
    }

    public void Kill() {
        Debug.Log("You died!");
        Destroy(livesUI[lives-1]);
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
