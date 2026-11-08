1. How does each pathfinding algorithms calculate and prioritize paths?
  BFS pathfinding prioritizes running faster compared other programs such as Dijkstra, however, it doesn't necessarily seek the shortest path to a destination as instead, it looks through each node using queues without any heuristic method. Dijkstra's goal is to look for the shortest path compared to BFS, so it looks for the point between the start and goal and finds the one that costs the least to take. A* calculates by providing the best guesses of the remaining distance based on the location of the object and goal at the time of calculating.

2. What challenges arise when dynamically updating obstacles in real-time?
  When using elements such as a NavMesh, dynamically updating obstacles can actively change the areas GameObjects can traverse on.

3. Which algorithm should you choose and how should you adapt it for larger grids or open-world settings?
  A* due to its high speed and ability to processes complex information in a short period of time. 

4. What would your approach be if you were to add weighted cells (e.g., "difficult terrain" areas)?
  Dijkstra, it allows the ability to hold information that would make the different effects on the different terrains to hold different movement costs and information.