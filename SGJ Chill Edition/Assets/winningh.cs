using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningh : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.transform.tag == "Player")
            {
                contact.otherCollider.transform.GetComponent<PlayerStats>().OnGameFinished(new PlayerStats[0]);
            }
        }
    }
}
