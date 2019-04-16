using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorPickerReceiver : MonoBehaviour
{
    public Renderer[] skinRends;
    private SkinColorChanger skinColorChanger = new SkinColorChanger();
    private ColorSave colorSave = new ColorSave();

    public ObjectToColor[] objectsToColor;

    void Start()
    {
        for (int i = 0; i < objectsToColor.Length; i++)
        {
            if(PlayerPrefs.GetString("CharColor"+i) != "")
            {
                GetColor(i);
            }
        }
        InitSkinColor();
        SkinColor();

        foreach (ObjectToColor objToColor in objectsToColor)
        {
            objToColor.picker.onValueChanged.AddListener(color =>
            {
                foreach(Renderer rend in objToColor.renderer)
                    rend.materials[objToColor.matId].color = color;
                objToColor.Color = color;
            });

            foreach (Renderer rend in objToColor.renderer)
                rend.materials[objToColor.matId].color = objToColor.picker.CurrentColor;

            objToColor.picker.CurrentColor = objToColor.Color;
        }
    }

    public void InitSkinColor()
    {
        for (int i = 0; i < skinRends.Length; i++)
        {
            skinColorChanger.AddSkinRenderer(skinRends[i]);
        }
    }

    public void ChangeSkinColor(float range)
    {
        skinColorChanger.ChangeSkin(range);
    }

    public void SaveColors()
    {
        //Hair and clothes
        int i = 0;
        foreach (ObjectToColor objToColor in objectsToColor)
        {
            colorSave.SaveColor("CharColor"+i, objToColor.Color);
            i++;
        }
        //Skin
        colorSave.SaveColor("SkinColor", skinColorChanger.GetColor());
    }

    public void GetColor(int index)
    {
        objectsToColor[index].Color = colorSave.GetSavedColor("CharColor" + index);
    }

    public void SkinColor()
    {
        if (PlayerPrefs.GetString("SkinColor") != "")
        {
            //Color skin = skinColorChanger.GetColor();
            Color skin = colorSave.GetSavedColor("SkinColor");
            skinColorChanger.curColor = skin;
            skinColorChanger.SetColor();
        }
    }

    [System.Serializable]
    public class ObjectToColor
    {
        public int matId = 0;
        public new Renderer[] renderer;
        public ColorPicker picker;

        public Color Color;
    }
}
