using UnityEngine.Networking;

public class ChatBase : MessageBase
{
    public string Sender;
    public string Text;
    public bool PassFroServer = false;
}