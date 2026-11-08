using System.Collections.Generic;
using UnityEngine;

public class GridGen : MonoBehaviour
{
    public GameObject tilePrefab;
    public float tileSize;
    public int width;
    public int height;
    public float obstaclesChance = 25f;
    public Vector2Int start = new Vector2Int(0, 1);
    public Vector2Int goal = new Vector2Int(4, 4);

    private int[,] grid;
    private List<Vector2Int> path = new List<Vector2Int>();
    private Dictionary<Vector2Int, GameObject> tileObjects = new Dictionary<Vector2Int, GameObject>();

    private Vector2Int current;
    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        GenerateGrid();
        FindPath(start, goal);
        DrawPath();
    }

    void GenerateGrid()
    {
        grid = new int[height, width];
        tileObjects.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int currentCoord = new Vector2Int(x, y);
                Vector3 spawnPosition = new Vector3(x * tileSize + (tileSize / 2), 0f, y * tileSize + (tileSize / 2));
                float placeObs = Random.Range(0, 100);

                GameObject spawnedTile = null;
                Renderer tileRenderer = null;

                if (x == start.x && y == start.y)
                {
                    grid[y, x] = 0;
                    spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
                    tileRenderer = spawnedTile.GetComponent<Renderer>();

                    if (tileRenderer != null) tileRenderer.material.color = Color.green;
                }
                else if (x == goal.x && y == goal.y)
                {
                    grid[y, x] = 0;
                    spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
                    tileRenderer = spawnedTile.GetComponent<Renderer>();

                    if (tileRenderer != null) tileRenderer.material.color = Color.red;
                }
                else
                {
                    if (placeObs <= obstaclesChance)
                    {
                        grid[y, x] = 1;
                        spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
                        tileRenderer = spawnedTile.GetComponent<Renderer>();

                        if (tileRenderer != null) tileRenderer.material.color = Color.black;
                    }
                    else
                    {
                        grid[y, x] = 0;
                        spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
                        tileRenderer = spawnedTile.GetComponent<Renderer>();

                        if (tileRenderer != null) tileRenderer.material.color = Color.white;
                    }
                }

                // Common settings and registering the tile into our dictionary
                if (tileRenderer != null)
                {
                    tileRenderer.material.SetFloat("_Glossiness", 0f);
                }

                if (spawnedTile != null)
                {
                    tileObjects[currentCoord] = spawnedTile; // <-- THIS SAVES THE TILE REFERENCE FOR DRAWPATH
                }
            }
        }
    }

    private bool IsInBounds(Vector2Int point)
    {
        if (grid == null) return false;
        return point.x >= 0 && point.x < grid.GetLength(1) && point.y >= 0 && point.y < grid.GetLength(0);
    }

    private void FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        frontier.Enqueue(startPos);

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        cameFrom[startPos] = startPos;

        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            if (current == goalPos)
            {
                break;
            }

            foreach (Vector2Int direction in directions)
            {
                Vector2Int next = current + direction;

                if (IsInBounds(next) && grid[next.y, next.x] == 0 && !cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goalPos))
        {
            Debug.Log("Path not found.");
            
            return;
        }

        // Trace path from goal to start
        Vector2Int step = goalPos;
        while (step != startPos)
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Add(startPos);
        path.Reverse();
    }

    private void DrawPath()
    {
        //Debug.Log("Path count to draw: " + path.Count); // Check if path has steps

        foreach (var step in path)
        {
            // Do not override start (green) or goal (red) colors
            if (step == start || step == goal) continue;

            if (tileObjects.ContainsKey(step))
            {
                GameObject tileObj = tileObjects[step];
                Renderer tileRenderer = tileObj.GetComponent<Renderer>();

                if (tileRenderer != null)
                {
                    tileRenderer.material.color = Color.blue;
                    //Debug.Log("Successfully colored tile at: " + step + " blue.");
                }
                else
                {
                    //Debug.LogWarning("Tile object at " + step + " found, but it has NO Renderer component!");
                }
            }
            else
            {
                //Debug.LogError("Dictionary does NOT contain key for step: " + step);
            }
        }
    
    }



}
