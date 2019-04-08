using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    RectTransform thrusterFuelFill;

    [SerializeField]
    GameObject pauseMenu;

    private PlayerController controller;

    public void SetController(PlayerController _controller)
    {
        controller = _controller;
    }

    private void Start()
    {
        PauseMenu.isOn = false;
    }

    private void Update()
    {
        if(thrusterFuelFill != null)
            SetFuelAmount(controller.GetThrusterFuelAmount());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Chat.isToggled == false)
                TogglePauseMenu();
        }
    }

    void SetFuelAmount(float _amount) //deprecated
    {
        thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }
}
