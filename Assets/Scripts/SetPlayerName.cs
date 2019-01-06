using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetPlayerName : MonoBehaviour
{
    public InputField nameInputText;
    string m_PlayerName;

    private void Start()
    {
        m_PlayerName = GetName(m_PlayerName);
        nameInputText.text = m_PlayerName;
    }

    public void SetName()
    {
        if (nameInputText.text != "")
        {
            m_PlayerName = nameInputText.text;
            PlayerPrefs.SetString("PlayerName", m_PlayerName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);

        }
    }

    private string GetName(string name)
    {
        name = PlayerPrefs.GetString("PlayerName");
        return name;
    }
}
