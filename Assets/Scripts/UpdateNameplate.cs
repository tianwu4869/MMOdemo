using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UpdateNameplate : NetworkBehaviour {

    [SyncVar(hook = "OnChangeName")]
    public string pname = "";

	void Update () {
        if (isLocalPlayer)
        {
            CmdFire(GameObject.Find("PlayerProfile").GetComponent<playerProfile>().pname);
        }
    }

    void OnChangeName(string pname)
    {
        this.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = pname;
        transform.root.name = pname;
    }

    [Command]
    void CmdFire(string clientName)
    {
        transform.root.name = clientName;
        string temp = clientName;
        pname = "";
        pname = temp;
    }

}
