using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {
    public Transform marble1;
    public Transform marble2;
    public Transform marble3;

    public Transform target1;
    public Transform target2;
    public Transform target3;

    float t;

    Vector3 ref1;
    Vector3 ref2;
    Vector3 ref3;

    bool move1;
    bool move2;
    bool move3;


    public void StartCountdown()
    {
        t = 5;
    }
    private void FixedUpdate()
    {
        if (t > 0)
        {
            t -= Time.deltaTime;

            if (t < 1)
            {
                marble3.GetComponent<Rigidbody>().isKinematic = true;
                move3 = true;
            }
            if (t < 2)
            {
                marble2.GetComponent<Rigidbody>().isKinematic = true;
                move2 = true;
            }
            if (t < 1)
            {
                marble1.GetComponent<Rigidbody>().isKinematic = true;
                move1 = true;
            }
        }

        if (move1) { marble1.position = Vector3.SmoothDamp(marble1.position, target1.position, ref ref1, 0.2f); marble1.Rotate(new Vector3(1, 0, 0), 90 * Time.deltaTime); }
        if (move2) { marble2.position = Vector3.SmoothDamp(marble2.position, target2.position, ref ref2, 0.2f); marble2.Rotate(new Vector3(1, 0, 0), 90 * Time.deltaTime); }
        if (move3) { marble3.position = Vector3.SmoothDamp(marble3.position, target3.position, ref ref3, 0.2f); marble3.Rotate(new Vector3(1, 0, 0), 90 * Time.deltaTime); }
    }
}
