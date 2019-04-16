using UnityEngine;

public class ColorSave
{
    public void SaveColor(string name, Color color)
    {
        PlayerPrefs.SetString(name, ColorToHex(color));
    }
    public Color GetSavedColor(string name)
    {
        Color GetedColor = HexToColor(PlayerPrefs.GetString(name));
        return GetedColor;
    }
    // Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
    public string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
