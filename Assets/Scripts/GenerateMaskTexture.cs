using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaskTexture : MonoBehaviour
{
    public Material mat;
    public int size = 64;

    public Texture2D tex;

    void Start()
    {
        tex = new Texture2D(size, size, TextureFormat.Alpha8, false, true);
        Color32[] colors = new Color32[size * size];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = new Color32(0, 0, 0, (byte)(i % 255));

        // shuffle pixels
        for (int i = 0; i < colors.Length - 1; i++)
        {
            Color32 tmp = colors[i];
            int rnd = Random.Range(i + 1, colors.Length);
            colors[i] = colors[rnd];
            colors[rnd] = tmp;
        }
        tex.SetPixels32(colors);
        tex.Apply();
        mat.SetTexture("_MaskTex", tex);
    }
}