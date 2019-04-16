using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TransimtColors : NetworkBehaviour
{
    //
    /*ColorPickerReceiver colorPickerReceiver;

    private void Start()
    {
        colorPickerReceiver = GetComponent<ColorPickerReceiver>();
        ClientGetColors();
    }

    [Client]
    public void ClientGetColors()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdGetColors();
    }

    [Command]
    void CmdGetColors()
    {
        RpcGetColors();
    }

    [ClientRpc]
    void RpcGetColors()
    {
        colorPickerReceiver.AssignSkinColor();
        for (int i = 0; i < colorPickerReceiver.objectsToColor.Length; i++)
        {
            colorPickerReceiver.AssignColor(i);
        }

    }*/
}
