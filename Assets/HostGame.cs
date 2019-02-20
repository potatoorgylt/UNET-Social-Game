using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 24;

    private string roomName;

    //private NetworkManager networkManager;
    public NetworkCustom customNetworkManager;

    public InputField hostGameInputField;

    private void Start()
    {
        if(PlayerPrefs.GetString("PlayerName") != "" || PlayerPrefs.GetString("PlayerName") != null)
        {
            string roomName = PlayerPrefs.GetString("PlayerName") + "'s room";
            hostGameInputField.text = roomName;
        }
        //networkManager = NetworkManager.singleton;
        /*if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }*/
        if(customNetworkManager.matchMaker == null)
        {
            customNetworkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _name)
    {
        roomName = _name;
    }

    public void CreateRoom()
    {
        if(roomName != "" && roomName != null)
        {
            //Create room
            //networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            customNetworkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, customNetworkManager.OnMatchCreate);
        }
    }
}
