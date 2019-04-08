using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayEmote : NetworkBehaviour
{
    Animator localAnimator;

    private void Start()
    {
        localAnimator = GetComponent<Animator>();    
    }

    [Client]
    public void AnimationPlay(int state)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdAnimationPlay(state);
    }

    [Command]
    void CmdAnimationPlay(int state)
    {
        RpcChatDialog(state);
    }

    [ClientRpc]
    void RpcChatDialog(int state)
    {
        if (state == 0)
        {
            localAnimator.SetTrigger("Laugh");
        }
        else if (state == 1)
        {
            localAnimator.SetTrigger("Happy");
        }
        else if (state == 2)
        {
            localAnimator.SetTrigger("Frustration");
        }
        else if (state == 3)
        {
            localAnimator.SetTrigger("Angry");
        }
        else if (state == 4)
        {
            localAnimator.SetTrigger("Cry");
        }
        else if (state == 5)
        {
            localAnimator.SetTrigger("Dance");
        }
    }
}
