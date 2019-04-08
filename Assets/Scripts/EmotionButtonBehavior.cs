using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EmotionButtonBehavior : NetworkBehaviour
{
    Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("Local start");
    }
}
