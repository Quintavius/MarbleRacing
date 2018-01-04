using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EggController : MonoBehaviour
{
    public float blastForce;
    public MarbleSkin[] loot;
    bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.transform.name == "Player")
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().AddExplosionForce(blastForce, other.transform.position, 5, 2);
                }
                GetComponent<AudioSource>().Play();
                FindObjectOfType<LootManager>().StartCountdown();

                foreach (MarbleSkin marble in loot)
                {
                    marble.GenerateLootSkin();
                }

                Destroy(transform.GetComponent<Collider>());
            }
        }
        else
        {
            triggered = true;
        }
    }
}
