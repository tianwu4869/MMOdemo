using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = transform.root.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (transform.root.name != col.transform.root.name)
        {
            //Debug.Log(transform.root.name + " Has detected a collider from someone else.");
            Animator enenmyAnim = col.transform.root.GetComponent<Animator>();
            if (enenmyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && col.name == "Bip001 Prop1")
            {
                Debug.Log(col.transform.root.name + " is attacking me.");
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knockback"))
                {
                    Debug.Log(transform.root.name + " Got hit.");
                    transform.root.GetComponent<Health>().TakeDamage(10, col.transform.root.transform.position);
                }
            }
        }
    }
}
