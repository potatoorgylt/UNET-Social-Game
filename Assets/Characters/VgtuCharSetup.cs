using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VgtuPlayer))]
public class VgtuCharSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private GameObject[] localObjectsToDisable;

    [SerializeField]
    private GameObject[] serverObjectsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";
    //[SerializeField]
    //GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;
    [SerializeField]
    GameObject playerUI3d;

    [HideInInspector]
    public GameObject playerUIInstance;

    [HideInInspector]
    public string _name;

    private void Start()
    {
        playerUI3d.GetComponent<LookAtObject>().target = Camera.main.transform;
        if (!isLocalPlayer)
        {
            DisableComponents();
            DisableGameObjects(serverObjectsToDisable);
            AssignRemoteLayer();
        }
        else
        {
            StartCoroutine(LateStart(0.1f));
            //Disable player graphics for local player
            //SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //Create player UI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            //Configure PlayerUI
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
                Debug.LogError("No player ui component on playerUi prefab");
            //ui.SetController(GetComponent<PlayerController>());

            GetComponent<VgtuPlayer>().SetupPlayer();

            DisableGameObjects(localObjectsToDisable);
        }
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _name = PlayerPrefs.GetString("PlayerName");
        GameObject.FindGameObjectWithTag("Chat").GetComponent<Chat>().ClientEventMessage(_name + " connected!", Chat.Message.MessageType.info);

        CmdSetNewUsername(_name);
    }

    [Command]
    private void CmdSetNewUsername(string value)
    {
        GetComponent<VgtuPlayer>().username = value;
    }

    /*public void SetLayerRecursively(GameObject obj, int newLayer) //Depracated
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }*/

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        VgtuPlayer _vgtuPlayer = GetComponent<VgtuPlayer>();

        GameManager.RegisterPlayer(_netID, null, _vgtuPlayer);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "LocalPlayer";
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void DisableGameObjects(GameObject[] objects)
    {
        for(int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);

        if (isLocalPlayer)
            GameManager.instance.SetSceneCameraActive(true);

        GameManager.UnRegisterVgtuPlayer(transform.name);
    }
}
