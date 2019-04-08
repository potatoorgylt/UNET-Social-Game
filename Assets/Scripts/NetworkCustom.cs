using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkCustom : NetworkManager
{

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int selectedClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();

        if (message.selectedClass == 0)
        {
            GameObject player = Instantiate(Resources.Load("VGTU_malev2_low", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        if (message.selectedClass == 1)
        {
            GameObject player = Instantiate(Resources.Load("VGTU_girl_low", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.selectedClass = PlayerPrefs.GetInt("CharacterSelected");
        
        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
}