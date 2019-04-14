using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorPickerReceiver : MonoBehaviour
{
    public Renderer[] skinRends;
    private SkinColorChanger skinColorChanger = new SkinColorChanger();

    public ObjectToColor[] objectsToColor;

    void Start()
    {
        InitSkinColor();

        foreach(ObjectToColor objToColor in objectsToColor)
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

    [System.Serializable]
    public class ObjectToColor
    {
        public int matId = 0;
        public new Renderer[] renderer;
        public ColorPicker picker;

        public Color Color;
    }
}
