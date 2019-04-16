using UnityEngine;
using System.Collections.Generic;

public class SkinColorChanger
{
    private List<Renderer> skinRenderer = new List<Renderer>();

    public Renderer GetSkinRenderer(int id)
    {
        return skinRenderer[id];
    }
    public void SetSkinRenderer(int id, Renderer value)
    {
        skinRenderer[id] = value;
    }
    public void AddSkinRenderer(Renderer rend)
    {
        skinRenderer.Add(rend);
    }

    private Color startValue = new Color(1f, 1f, 1f, 1f);
    private Color endValue = new Color(22f/255f, 13f/255f, 0f, 1f);
    public Color curColor;

    public void ChangeSkin(float range)
    {
        curColor.r = Mathf.Lerp(endValue.r, startValue.r, 1f - range);
        curColor.g = Mathf.Lerp(endValue.g, startValue.g, 1f - range);
        curColor.b = Mathf.Lerp(endValue.b, startValue.b, 1f - range);

        SetColor();
    }

    public void SetColor()
    {
        for (int i = 0; i < skinRenderer.Count; i++)
        {
            skinRenderer[i].materials[2].color = curColor;
        }
    }

    public Color GetColor()
    {
        return curColor;
    }
}
