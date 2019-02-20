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

        btn1.onClick.AddListener(delegate { SelectChar(0); });
        btn2.onClick.AddListener(delegate { SelectChar(1); });
    }

    void SelectChar(int charId)
    {
        if(charId == 0)
        {
            networkManager.btn1();
        }
        else if(charId == 1)
        {
            networkManager.btn2();
        }
    }
}
