using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public MatchSettings matchSettings;

    [SerializeField]
    private GameObject sceneCamera;

    public bool toggleChatOnStart = false;

    //public string clientUsername = "Anonymous";

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
        }
        else
        {
            instance = this;
        }
    }

    public void SetSceneCameraActive(bool isActive)
    {
        if(sceneCamera == null)
        {
            return;
        }
        sceneCamera.SetActive(isActive);
    }

    #region Player tracking
    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();//deprecated
    private static Dictionary<string, VgtuPlayer> vgtuPlayers = new Dictionary<string, VgtuPlayer>();

    public static void RegisterPlayer(string _netID, Player _player, VgtuPlayer _vgtuPlayer)
    {
        if(_player != null)//deprecated
        {
            string _playerID = PLAYER_ID_PREFIX + _netID;

            players.Add(_playerID, _player);
            _player.transform.name = _playerID;
        }
        else if (_vgtuPlayer != null)
        {
            string _playerID = PLAYER_ID_PREFIX + _netID;

            vgtuPlayers.Add(_playerID, _vgtuPlayer);
            _vgtuPlayer.transform.name = _playerID;
        }
    }

    public static void UnRegisterPlayer(string _playerID) //deprecated
    {
            players.Remove(_playerID);
    }


    public static void UnRegisterVgtuPlayer(string _playerID)
    {
        vgtuPlayers.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID) //deprecated
    {
        return players[_playerID];
    }

    public static VgtuPlayer GetVgtuPlayer(string _playerID)
    {
        return vgtuPlayers[_playerID];
    }

    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach(string _playerID in players.Keys)
        {
            GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
    #endregion
}
