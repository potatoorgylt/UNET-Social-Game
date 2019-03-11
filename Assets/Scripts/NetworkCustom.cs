using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkCustom : NetworkManager
{

    public int chosenCharacter = 0;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);

        selectedClass = PlayerPrefs.GetInt("CharacterSelected");

        if (selectedClass == 0)
        {
            GameObject player = Instantiate(Resources.Load("VGTU_malev2_low", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        if (selectedClass == 1)
        {
            GameObject player = Instantiate(Resources.Load("VGTU_girl_low", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = PlayerPrefs.GetInt("CharacterSelected");

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public void btn1() //deprecated
    {
        chosenCharacter = 0;
        Debug.Log("You chose first charecter");
    }

    public void btn2() //deprecated
    {
        chosenCharacter = 1;
        Debug.Log("You chose second charecter");
    }
}