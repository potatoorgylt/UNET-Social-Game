using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

[RequireComponent(typeof(NetworkIdentity))]
public class Chat : NetworkBehaviour
{
    public string playerName = "user";

    public static bool isToggled = false;

    public GameObject chatContent, textObject;
    public InputField chatInputField;
    private GameObject chatPanel;
    public Scrollbar scrollbarVertical;
    [SerializeField]
    private Text chatArea;

    public Color playerMessage, info;

    //Messages
    public int maxMessages = 25;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    //Networking
    const short chatMsg = 1000;
    NetworkClient _client;

    [SerializeField]
    private SyncListString chatLog = new SyncListString();

    public override void OnStartClient()
    {
        chatLog.Callback = OnChatUpdated; // set chatlog callback to OnChatUpdated
    }

    private void Awake()
    {
        chatPanel = this.transform.GetChild(0).gameObject;
        //ToggleChat(false);
    }

    public void Start()
    {
        _client = NetworkManager.singleton.client; // get the NetworkClient from NetworkManager
        NetworkServer.RegisterHandler(chatMsg, OnServerPostChatMessage); // register network handler for chatMsg
        chatInputField.onEndEdit.AddListener(delegate { PostMessageToChat(chatInputField.text, Message.MessageType.playerMessage); });

        playerName = PlayerPrefs.GetString("PlayerName");

        ToggleChat(GameManager.instance.toggleChatOnStart);
    }

    private void Update()
    {
        if (chatInputField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                chatInputField.text = "";
                ToggleChat(false);
            }
        }
        else
        {
            if (!chatInputField.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                ToggleChat(true);
                chatInputField.ActivateInputField();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleChat(false);
        }
    }
    [Client]
    private void PostMessageToChat(string message, Message.MessageType messageType)
    {
        if (chatInputField.text != "")
        {
            ToggleChat(!isToggled);

            Message newMessage = new Message();
            newMessage.text = message;

            messageList.Add(newMessage);

            //string playerName = GameManager.instance.clientUsername;

            StringMessage msg;
            msg = new StringMessage("<color=#" + ColorUtility.ToHtmlStringRGBA(playerMessage) + ">[" + playerName + "]</color>: " + "<color=#" + ColorUtility.ToHtmlStringRGBA(playerMessage) + ">" + message + "</color>");

            _client.Send(chatMsg, msg);
        }
    }

    [Client]
    public void ClientConnectedMessage(string playerName, Message.MessageType messageType)
    {
        Message newMessage = new Message();
        newMessage.text = playerName;

        messageList.Add(newMessage);

        //string playerName = GameManager.instance.clientUsername;

        StringMessage msg;
        msg = new StringMessage(playerName + " connected!");

        _client.Send(chatMsg, msg);
    }

    [Server]
    void OnServerPostChatMessage(NetworkMessage netMsg)
    {
        string message = netMsg.ReadMessage<StringMessage>().value;
        chatLog.Add(message);
    }
    private void OnChatUpdated(SyncListString.Operation op, int index)
    {
        chatArea.text += chatLog[chatLog.Count - 1] + "\n";
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
        }

        return color;
    }

    public void ToggleChat(bool cond)
    {
        chatInputField.gameObject.SetActive(cond);
        this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = cond;
        scrollbarVertical.GetComponent<Image>().enabled = cond;
        scrollbarVertical.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = cond;
        isToggled = cond;
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public MessageType messageType;

        public enum MessageType
        {
            playerMessage,
            info
        }
    }
}