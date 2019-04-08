using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerCanvas : NetworkBehaviour
{
    [SerializeField]
    private Text usernameText;

    [SerializeField]
    private GameObject chatPanel;
    private Text panelText;

    [SerializeField]
    private Player player;
    [SerializeField]
    private VgtuPlayer vgtuPlayer;

    private Chat chat;
    private InputField chatInput;

    private bool toggleChat = true;

    private string messageToDisplay;

    private void Start()
    {
        chat = GameObject.FindWithTag("Chat").GetComponent<Chat>();
        panelText = chatPanel.GetComponentInChildren<Text>();
        chatInput = chat.transform.GetChild(0).GetChild(1).GetComponent<InputField>();
        chatInput.onEndEdit.AddListener(delegate { StartMessage(); });
    }

    private void Update()
    {
        if(player != null)
            usernameText.text = player.username;
        if (vgtuPlayer != null)
            usernameText.text = vgtuPlayer.username;

        /*if (chat.currMessage != "")
        {
            if (toggleChat == true)
            {
                StartCoroutine("ShowMessage");
                    
            }
            if (tempMessage != chat.currMessage)
            {
                StopCoroutine("ShowMessage");
                chatPanel.SetActive(false);
                toggleChat = true;
            }
        }*/
    }

    void StartMessage()
    {
        if (chatInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            messageToDisplay = chatInput.text;
            StopCoroutine("ShowMessage");
            StartCoroutine("ShowMessage");
        }
    }

    private IEnumerator ShowMessage()
    {
        ChatDialog(false);
        yield return new WaitForSeconds(5.0f);
        ChatDialog(true);
    }

    [Client]
    void ChatDialog(bool toggleChat)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        CmdChatDialog(messageToDisplay, toggleChat);
    }

    [Command]
    void CmdChatDialog(string message, bool toggleChat)
    {
        RpcChatDialog(message, toggleChat);
    }

    [ClientRpc]
    void RpcChatDialog(string message, bool toggleChat)
    {
        //tempMessage = chat.currMessage; //should be in update //deprecated
        this.toggleChat = toggleChat;
        chatPanel.SetActive(!toggleChat);

        if (toggleChat == false)
            panelText.text = message;
    }
}
