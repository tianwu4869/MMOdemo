using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerProfile : MonoBehaviour {

    public string pname = "";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Enter " + scene.name);
        if (string.Compare(scene.name, "Battlefield") == 0)
        {
            if (string.Compare(pname, "Tian Wu") == 0)
            {
                GameObject.Find("Network Manager").GetComponent<CustomNetworkManager>().SetupServer();
            }
            else if (string.Compare(pname, "") != 0)
            {
                GameObject.Find("Network Manager").GetComponent<CustomNetworkManager>().SetupClient();
            }
        }
    }
}
