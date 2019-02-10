using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGravity : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        //collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
