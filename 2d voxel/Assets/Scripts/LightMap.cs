using UnityEngine;

public static class LightMap
{
    public static Texture2D lighMapTexture;
    public static Texture2D voxelRenderTexture;


    public static void Generate(Texture2D LMTexture, Texture2D VRTexture)
    {
        lighMapTexture = LMTexture;
        voxelRenderTexture = VRTexture;
    }


    public static void Update(Vector2Int start, byte[,] lightData)
    {
        int width = lightData.GetLength(0);
        int height = lightData.GetLength(1);

        Color32[] colorMap = new Color32[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color lightLevel = Color.black;
                lightLevel.a = lightData[width - 1 - x, height - 1 - y] / 10f;

                colorMap[y * width + x] = lightLevel;
            }
        }

        lighMapTexture.SetPixels32(start.x, start.y, width, height, colorMap);
        lighMapTexture.Apply();

        voxelRenderTexture.SetPixels32(start.x, start.y, width, height, colorMap);
        voxelRenderTexture.Apply();
    }
}
