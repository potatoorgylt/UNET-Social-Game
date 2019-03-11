using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [System.Serializable]
    public class Character
    {
        public string resourceName;
        public GameObject selectionObj;
        public Toggle characterToggle;
    }

    public List<Character> characters;

    private void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("CharacterSelected");
        characters[selectedIndex].characterToggle.isOn = true;
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("CharacterSelected"));
        for (int i = 0; i < characters.Count; i++)
        {
            if(characters[i].characterToggle.isOn == true)
            {
                characters[i].selectionObj.SetActive(true);
                PlayerPrefs.SetInt("CharacterSelected", i);
            }
            else
            {
                characters[i].selectionObj.SetActive(false);
            }
        }
    }
}

