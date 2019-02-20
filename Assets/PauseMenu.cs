using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour
{
    public static bool isOn = false;

    private GameObject networkManagerObj;
    private NetworkCustom networkManager;

    private void Start()
    {
        //networkManager = NetworkManager.singleton;
        networkManagerObj = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = networkManagerObj.GetComponent<NetworkCustom>();
        Debug.Log("Getting net manager: " + networkManager);
        Debug.Log( networkManager);
    }

    public void LeaveRoom()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }
}
