using UnityEngine;

public class PathTester : MonoBehaviour
{
    public Pathfinding pathfinding;

    void Update()
    {
        // Press P to change the start and goal
        if (Input.GetKeyDown(KeyCode.P))
        {
            pathfinding.SetStartAndGoal(
                new Vector2Int(2, 0),
                new Vector2Int(4, 3)
            );


            Debug.Log("Changed start and goal!");
        }
    }
}
