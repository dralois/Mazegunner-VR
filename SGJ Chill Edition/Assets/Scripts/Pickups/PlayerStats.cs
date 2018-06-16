using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

    float shieldTimer = 0.0f;
    public GameObject shieldPrefab;
    private GameObject shield;

    float invisibilityTimer = 0.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (shieldTimer > 0.0f) {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer < 0.0f) {
                Destroy(shield);
            }
        }

        if (invisibilityTimer > 0.0f) {
            invisibilityTimer -= Time.deltaTime;

            if (invisibilityTimer < 0.0f) {
                GetComponentInChildren<Renderer>().enabled = true;

                if (shieldTimer > 0.0f) {
                    shield.GetComponent<Renderer>().enabled = true;
                }
            }
        }
    }

    public void SpeedBoost(float amount, float time) {
        Debug.Log("Speed");
    }

    public void SpeedBuff(float amount, float time) {

    }

    public void Invisibility(float time) {
        invisibilityTimer = time;
        GetComponentInChildren<Renderer>().enabled = false;
    }

    public void Shield(float time) {
        shieldTimer = time;
        shield = Instantiate(shieldPrefab, transform);
        if (invisibilityTimer > 0.0f) {
            shield.GetComponent<Renderer>().enabled = false;
        }
    }
}
