using UnityEngine;

public class GridGen : MonoBehaviour
{
    public GameObject tilePrefab;
    public float tileSize;
    public int width;
    public int height;
    public float obstaclesChance = 25f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 spawnPosition = new Vector3(x * tileSize + (tileSize/2), 0f, y * tileSize + (tileSize/2));
                float placeObs = Random.Range(0,100);
                if (placeObs <= obstaclesChance)
                {
                    Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
                }
                Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}
