using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btn1;
    public Button btn2;

    private GameObject networkManagerObj;
    private NetworkCustom networkManager;

    // Start is called before the first frame update
    void Start()
    {
        networkManagerObj = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = networkManagerObj.GetComponent<NetworkCustom>();
    }
}
