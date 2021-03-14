using UnityEngine;

public static class LightMap
{
    public static Texture2D lightMapTexture;
    public static Texture2D blockRenderTexture;
    public static Color lightLevel;


    public static void Generate(Texture2D LMTex, Texture2D BRTex)
    {
        lightMapTexture = LMTex;
        blockRenderTexture = BRTex;

        lightLevel = Color.black;
    }

    public static void Update(int sx, int sy, int[,] lightData)
    {
        if (lightMapTexture == null || blockRenderTexture == null) return;

        int width = lightData.GetLength(0);
        int height = lightData.GetLength(1);

        Color32[] colorMap = new Color32[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                lightLevel.a = lightData[width - 1 - x, height - 1 - y] / 10f;

                colorMap[y * width + x] = lightLevel;
            }
        }

        lightMapTexture.SetPixels32(sx, sy, width, height, colorMap);
        lightMapTexture.Apply();
        blockRenderTexture.SetPixels32(sx, sy, width, height, colorMap);
        blockRenderTexture.Apply();
    }
}
