using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{

    float shieldTimer = 0.0f;
    public GameObject shieldPrefab;
    private GameObject shield;

    float invisibilityTimer = 0.0f;
    float speedTimer = 0.0f;
    float slowTimer = 0.0f;

    private float health;
    public float fullHealth = 100.0f;
    private int lives = 3;

    float score;
    float gameDuration;

    private int originalLayer;

    private GameObject lastCheckpoint;
    private PlayerMovement movement;

    private GameObject[] livesUI = new GameObject[3];
    private GameObject invisUI;
    private GameObject speedUI;
    private GameObject slowUI;
    private GameObject shieldUI;
    private GameObject lostUI;

    // Use this for initialization
    void Start()
    {
        health = fullHealth;
        originalLayer = gameObject.layer;
        movement = GetComponent<PlayerMovement>();

        Canvas can = GetComponentInChildren<Canvas>();
        livesUI[0] = can.transform.Find("Live0").gameObject;
        livesUI[1] = can.transform.Find("Live1").gameObject;
        livesUI[2] = can.transform.Find("Live2").gameObject;

        livesUI[0].SetActive(true);
        livesUI[1].SetActive(true);
        livesUI[2].SetActive(true);

        invisUI = can.transform.Find("Invisibility").gameObject;
        speedUI = can.transform.Find("SpeedBoost").gameObject;
        slowUI = can.transform.Find("SlowDown").gameObject;
        shieldUI = can.transform.Find("Shield").gameObject;

        invisUI.SetActive(false);
        speedUI.SetActive(false);
        slowUI.SetActive(false);
        shieldUI.SetActive(false);

        lostUI = can.transform.Find("Lost").gameObject;
        lostUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 1)
        {
            return;
        }

        if (shieldTimer > 0.0f)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer < 0.0f)
            {
                shieldUI.SetActive(false);
                Destroy(shield);
            }
        }

        if (invisibilityTimer > 0.0f)
        {
            invisibilityTimer -= Time.deltaTime;

            if (invisibilityTimer < 0.0f)
            {
                CmdResetInvisibility();
                invisUI.SetActive(false);
            }
        }

        if (speedTimer > 0.0f) {
            speedTimer -= Time.deltaTime;

            if (speedTimer < 0.0f) {
                speedUI.SetActive(false);
            }
        }

        if (slowTimer > 0.0f) {
            slowTimer -= Time.deltaTime;

            if (slowTimer < 0.0f) {
                slowUI.SetActive(false);
            }
        }
    }

    public void SpeedBoost(float amount, float time)
    {
        speedTimer = time;
        movement.Speedbuff(amount, time);
        speedUI.SetActive(true);
    }

    //Slowdown
    public void SpeedBuff(float amount, float time)
    {
        slowTimer = time;
        movement.SlowDown(amount, time);
        slowUI.SetActive(true);
    }

    [Command]
    public void CmdResetInvisibility()
    {
        gameObject.layer = originalLayer;

        if (shieldTimer > 0.0f)
        {
            shield.layer = originalLayer;
        }
    }

    [Command]
    public void CmdInvisibility(float time)
    {
        invisibilityTimer = time;
        gameObject.layer = 9; // Only PC layer
        invisUI.SetActive(true);
    }

    public void Shield(float time)
    {
        shieldTimer = time;
        shield = Instantiate(shieldPrefab, transform);
        if (invisibilityTimer > 0.0f)
        {
            //shield.GetComponent<Renderer>().enabled = false;
            shield.layer = 9; // Only PC layer
        }
        shieldUI.SetActive(true);
    }

    public void Kill()
    {
        Debug.Log("You died!");
        lives--;
        livesUI[lives].SetActive(false);        
        Respawn(active: (lives > 0));
    }

    public void Damage(float amount)
    {
        health -= amount;

        if (health < 0.0f)
        {
            Kill();
        }
    }

    private void Respawn(bool active)
    {
        Debug.Log(active);

        health = fullHealth;

        if (lastCheckpoint == null)
        {
            ExtendedNetworkManager nm = (ExtendedNetworkManager)NetworkManager.singleton;
            transform.position = nm.PCPlayerSpawns.getPlayerSpawn();
        }
        else
        {
            transform.position = lastCheckpoint.transform.position;
        }

        if (!active)
        {
            GetComponent<PlayerMovement>().enabled = false;
            lostUI.SetActive(true);
        }
    }

    public void OnGameStarted(int lives, float gameDuration)
    {
        this.lives = lives;
        this.gameDuration = gameDuration;
    }

    public void OnGameFinished(PlayerStats[] allStats)
    {
        // TODO Display scores
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            print("found Checkpoint");
            lastCheckpoint = other.gameObject;
        }
    }
}
