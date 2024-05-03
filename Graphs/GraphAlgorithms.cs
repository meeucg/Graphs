public class GraphAlgorithms
{
    public List<int> DijkstraAllOnes(IGraph graph, int start) //в случае если все веса ребер неориентированного графа = 1 
    {
        if (!graph.GetVertices.Contains(start)) 
        { 
            throw new Exception(message:$"Вершины {nameof(start)} нет в графе{nameof(graph)}"); 
        }

        Dictionary<int, bool> isVisited = new();
        Dictionary<int, int> minDistance = new();
        int current = start;

        Queue<int> order = new();
        order.Enqueue(start);
        foreach (var i in graph.GetVertices)
        {
            isVisited[i] = false;
        }
        isVisited[start] = true;
        minDistance[start] = 0;

        while(order.TryDequeue(out current))
        {        
            foreach (int i in graph.GetAllConnectedVertices(current))
            {
                if (!isVisited[i])
                {
                    isVisited[i] = true;
                    minDistance[i] = minDistance[current] + 1;
                    order.Enqueue(i);
                }
                else 
                {
                    minDistance[i] = Math.Min(minDistance[current] + 1, minDistance[i]);
                }
            }
        }
        return minDistance.Values.ToList();
    }

    public List<int> DijkstraWeighted(IWeightedGraph graph, int start) //неориентированный взвешенный граф 
    {
        if (!graph.GetVertices.Contains(start))
        {
            throw new Exception(message: $"Вершины {nameof(start)} нет в графе{nameof(graph)}");
        }

        Dictionary<int, bool> isVisited = new();
        Dictionary<int, int> minDistance = new();
        int current = start;

        Queue<int> order = new();
        order.Enqueue(start);
        foreach (var i in graph.GetVertices)
        {
            isVisited[i] = false;
        }
        isVisited[start] = true;
        minDistance[start] = 0;

        while (order.TryDequeue(out current))
        {
            foreach (int i in graph.GetAllConnectedVertices(current))
            {
                if (!isVisited[i])
                {
                    isVisited[i] = true;
                    minDistance[i] = minDistance[current] + graph.GetEdgeWeight((current, i));
                    order.Enqueue(i);
                }
                else
                {
                    minDistance[i] = Math.Min(minDistance[current] + graph.GetEdgeWeight((current, i))
                        , minDistance[i]);
                }
            }
        }
        return minDistance.Values.ToList();
    }
}