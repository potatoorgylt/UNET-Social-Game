using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerNameplate : NetworkBehaviour
{

    [SerializeField]
    private Text usernameText;

    [SerializeField]
    private Player player;

    private void Update()
    {
        usernameText.text = player.username;
    }
}
