using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkManager
{
    public NetworkClient myClient;

    public class MyMsgType
    {
        public static short Name = MsgType.Highest + 1;
    };

    public class NameMessage : MessageBase
    {
        public string name;
    }

    public void SetupServer()
    {
        StartServer();
        NetworkServer.RegisterHandler(MyMsgType.Name, OnReceiveName);
    }

    public void SetupClient()
    {
        StartClient();
    }

    public override void OnStartServer()
    {
        Debug.Log("Server has started.");
    }

    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("Client has started");
        myClient = client;
        client.Connect("127.0.0.1", 4869);
    }

    public void OnReceiveName(NetworkMessage netMsg)
    {
        NameMessage msg = netMsg.ReadMessage<NameMessage>();
        if (GameObject.Find("OnlineUsersRecord"))
        {
            Debug.Log("Record found.");
        }
        else
        {
            Debug.Log("Record not found.");
        }
        if (GameObject.Find("PlayerProfile"))
        {
            Debug.Log("PlayerProfile found.");
        }
        else
        {
            Debug.Log("PlayerProfile not found.");
        }
        GameObject.Find("OnlineUsersRecord").GetComponent<OnlineUsersTable>().AddUser(msg.name, netMsg.conn.connectionId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");
        NameMessage msg = new NameMessage();
        msg.name = GameObject.Find("PlayerProfile").GetComponent<playerProfile>().pname;
        myClient.Send(MyMsgType.Name, msg);

        ClientScene.Ready(conn);
        ClientScene.AddPlayer(conn, 0);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        //Change the text to show the connection loss on the client side
        Debug.Log("Client Side : Client " + conn.connectionId + " Lost!");
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        Debug.Log("Client network error occurred: " + (NetworkError)errorCode);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("A client connected to the server: " + conn);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        NetworkServer.SetClientReady(conn);
        Debug.Log("Client is set to the ready state (ready to receive state updates): " + conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(0, -2.475f, 0), Quaternion.identity);
        //player.transform.name = GameObject.Find("OnlineUsersRecord").GetComponent<OnlineUsersTable>().getNameFromNewUser();
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        Debug.Log("Client has requested to get his player added to the game");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkServer.DestroyPlayersForConnection(conn);
        GameObject.Find("OnlineUsersRecord").GetComponent<OnlineUsersTable>().RemoveUser(conn.connectionId);
        Debug.Log("A client disconnected from the server: " + conn);
    }
}
