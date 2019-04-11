using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using Cinemachine;

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

    [SerializeField]
    private GameObject emotionPanel;
    private bool emotionPanelToggle = true;

    [SerializeField]
    private GameObject emotionButton;

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
        else if (chatInputField.text == "" && Input.GetKeyDown(KeyCode.Return))
        {
            ToggleChat(!isToggled);
        }
        if (emotionButton.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleChat(false);
            emotionPanelToggle = true;
            //isToggled = true;
        }
        if (!chatInputField.isFocused)
        {
            chatInputField.ActivateInputField();   
        }
    }
    [Client]
    private void PostMessageToChat(string message, Message.MessageType messageType)
    {
        if (chatInputField.text != "" && Input.GetKeyDown(KeyCode.Return))
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
    public void ClientEventMessage(string playerMessage, Message.MessageType messageType)
    {
        Message newMessage = new Message();
        newMessage.text = playerMessage;

        messageList.Add(newMessage);

        //string playerName = GameManager.instance.clientUsername;

        StringMessage msg;
        msg = new StringMessage(playerMessage);

        _client.Send(chatMsg, msg);
    }



    [Server]
    void OnServerPostChatMessage(NetworkMessage netMsg)
    {
        string message = netMsg.ReadMessage<StringMessage>().value;
        //currMessage = message; //deprecated
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
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = cond;
        scrollbarVertical.GetComponent<Image>().enabled = cond;
        scrollbarVertical.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = cond;
        emotionButton.SetActive(cond);
        emotionPanel.SetActive(false);

        CameraControl.instance.EnableAxis(!cond, "Mouse X");
        CameraControl.instance.EnableAxis(!cond, "Mouse Y");

        isToggled = cond;
    }

    public void ToggleEmotionPanel()
    {
        emotionPanel.SetActive(emotionPanelToggle);
        if (emotionPanelToggle == true)
            emotionPanelToggle = false;
        else
            emotionPanelToggle = true;
    }

    GameObject localPlayer;
    Animator localAnimator;

    public void PlayAnimation(int state)
    {
        localPlayer = GameObject.Find("LocalPlayer");
        string playerName = localPlayer.GetComponent<VgtuCharSetup>()._name;

        switch (state)
        {
            case 0:
                localPlayer.GetComponent<PlayEmote>().AnimationPlay(state);
                ClientEventMessage(playerName + " is laughing from something.", Message.MessageType.info);
                break;
            case 1:
                localPlayer.GetComponent<PlayEmote>().AnimationPlay(state);
                ClientEventMessage(playerName + " is extraordinary happy.", Message.MessageType.info);
                break;
            case 2:
                localPlayer.GetComponent<PlayEmote>().AnimationPlay(state);
                ClientEventMessage(playerName + " feels frustrated as hell.", Message.MessageType.info);
                break;
            case 3:
                localPlayer.GetComponent<PlayEmote>().AnimationPlay(state);
                ClientEventMessage(playerName + " is pissed", Message.MessageType.info);
                break;
            case 4:
                localPlayer.GetComponent<PlayEmote>().AnimationPlay(state);
                ClientEventMessage(playerName + " is crying eyes out.", Message.MessageType.info);
                break;
            case 5:
                localPlayer.GetComponent<PlayEmote>().AnimationPlay(state);
                ClientEventMessage(playerName + " has smooth moves.", Message.MessageType.info);
                break;
            default:
                Debug.LogError("Animation state out of bounds");
                break;

        }
        ToggleEmotionPanel();
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