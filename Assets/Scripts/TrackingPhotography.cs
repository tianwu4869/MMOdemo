using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrackingPhotography : NetworkBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            Camera.main.transform.position = transform.position - transform.forward * 4 + new Vector3(0, 3.0f, 0);
            Camera.main.transform.eulerAngles = transform.eulerAngles + new Vector3(20.0f, 0, 0);
        }
    }
}
