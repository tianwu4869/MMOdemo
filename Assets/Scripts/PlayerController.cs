using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Animator anim;

    [ClientRpc]
    void RpcSyncAttack()
    {
        anim.Play("Attack", -1, 0f);
    }

    [Command]
    void CmdSyncAttack()
    {
        anim.Play("Attack", -1, 0f);
        RpcSyncAttack();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSyncAttack();
            //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            //{
            //  anim.Play("Attack", -1, 0f);
            //}
        }

        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);

        var x = inputH * Time.deltaTime * 150.0f;
        var z = 0.0f;
        if (inputV > 0)
        {
            z = inputV * Time.deltaTime * 7.0f;
        }
        else
        {
            z = inputV * Time.deltaTime * 2.0f;
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }
	}
}
