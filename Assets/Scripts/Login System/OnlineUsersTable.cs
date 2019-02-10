using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineUsersTable : MonoBehaviour
{

    public class user
    {
        public string name;
        public int connectionId;
        public user(string n, int id)
        {
            name = n;
            connectionId = id;
        }
    };

    public List<user> onlineUsers = new List<user>();
    //private string phpURL1 = "http://localhost/clientOffline.php";
    private string phpURL1 = "http://localhost:8080/JavaBridge/clientOffline.php";

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    } 

    public string getNameFromNewUser()
    {
        return onlineUsers[onlineUsers.Count - 1].name;
    }

    public void AddUser(string name, int connectionId)
    {
        onlineUsers.Add(new user(name, connectionId));
        Debug.Log("Number of users: " + onlineUsers.Count);
    }

    public void RemoveUser(int connectionId)
    {
        string disconnectedUser = "";
        for (int i = 0; i < onlineUsers.Count; i++)
        {
            if (onlineUsers[i].connectionId == connectionId)
            {
                disconnectedUser = onlineUsers[i].name;
                onlineUsers.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < onlineUsers.Count; i++)
        {
            Debug.Log(onlineUsers[i].name + "    " + onlineUsers[i].connectionId);
        }
        Debug.Log("Bye, " + disconnectedUser);

        WWWForm form = new WWWForm();
        form.AddField("name", disconnectedUser);

        WWW www = new WWW(phpURL1, form);
        StartCoroutine(connect(www));
    }

    IEnumerator connect(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void OnApplicationQuit()
    {
        if (GameObject.Find("PlayerProfile").GetComponent<playerProfile>().pname == "Tian Wu")
        {
            Debug.Log("Server has been shut down.");
            WWWForm form = new WWWForm();
            form.AddField("name", "Tian Wu");

            WWW www = new WWW(phpURL1, form);
            StartCoroutine(connect(www));
        }
    }
}
