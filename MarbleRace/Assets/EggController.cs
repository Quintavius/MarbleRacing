using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EggController : MonoBehaviour {
    public float blastForce;

	// Use this for initialization
	void Start () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().AddExplosionForce(blastForce, other.transform.position, 5,2);
            }
            GetComponent<AudioSource>().Play();
            FindObjectOfType<LootManager>().StartCountdown();
            Destroy(transform.GetComponent<Collider>());
        }
    }
}
