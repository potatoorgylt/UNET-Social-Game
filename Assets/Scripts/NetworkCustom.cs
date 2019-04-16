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
        public string[] charColor = new string[2];
        public string skinColor = "SkinColor";
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        GameObject playerObj = null;

        if (message.selectedClass == 0)
        {
            playerObj = Instantiate(Resources.Load("VGTU_malev2_low", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, playerObj, playerControllerId);
        }

        if (message.selectedClass == 1)
        {
            playerObj = Instantiate(Resources.Load("VGTU_girl_low", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, playerObj, playerControllerId);
        }

        //playerObj.GetComponent<ColorPickerReceiver>().;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage netMsg = new NetworkMessage();
        netMsg.selectedClass = PlayerPrefs.GetInt("CharacterSelected");
        for (int i = 0; i < 3; i++)
        {
            netMsg.charColor[i] = PlayerPrefs.GetString("CharColor" + i);
        }
        netMsg.skinColor = PlayerPrefs.GetString("SkinColor");

        ClientScene.AddPlayer(conn, 0, netMsg);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
}