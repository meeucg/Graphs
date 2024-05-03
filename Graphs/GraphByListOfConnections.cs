namespace GraphByList;

public class GraphByListOfConnections : IGraph
{
    public GraphByListOfConnections(List<List<int>> listOfConnections)
    {
        VerticeConnections = listOfConnections;
    }

    public GraphByListOfConnections(IGraph graph)
    {
        UniteGraphs(graph);
    }

    private List<List<int>> VerticeConnections = new();

    public int EdgesCount => VerticeConnections.Sum(t => t.Count);

    public HashSet<(int, int)> GetEdges
    {
        get
        {
            var Edges = new HashSet<(int, int)>();
            for (int i = 0; i < VerticesCount; i++)
            {
                foreach (var j in VerticeConnections[i])
                {
                    Edges.Add((i, j));
                }
            }
            return Edges;
        }
    }

    public IEnumerable<int> GetVertices
    {
        get
        {
            var arr = new int[VerticesCount];
            for (int i = 0; i < VerticesCount; i++)
            {
                arr[i] = i;
            }
            return arr;
        }
    }

    public int VerticesCount => VerticeConnections.Count;

    public bool AddEdge((int vertice1, int vertice2) Edge)
    {
        if (Edge.vertice1 < VerticesCount && Edge.vertice2 < VerticesCount)
        {
            VerticeConnections[Edge.vertice1].Add(Edge.vertice2);
            return true;
        }
        return false;
    }

    public bool AddVertice(params object[] Params)
    {
        foreach (int i in Params)
        {
            if (i > VerticesCount)
            {
                return false;
            }
        }
        foreach (int i in Params)
        {
            VerticeConnections[i].Add(VerticesCount);
        }
        VerticeConnections.Add(Params.Select(t => (int)t).ToList());
        return true;
    }

    public IEnumerable<int> GetAllConnectedVertices(int vertice)
    {
        return VerticeConnections[vertice];
    }

    public IEnumerable<(int, int)> GetAllIncidentEdges(int vertice)
    {
        return VerticeConnections[vertice].Select(t => (vertice, t));
    }

    public bool RemoveEdge((int vertice1, int vertice2) Edge)
    {
        if (VerticeConnections[Edge.vertice1].Remove(Edge.vertice2)) { }
        else
        {
            return false;
        }
        if (VerticeConnections[Edge.vertice2].Remove(Edge.vertice1)) { }
        else
        {
            VerticeConnections[Edge.vertice1].Add(Edge.vertice2);
            return false;
        }
        return true;
    }

    public bool RemoveVertice(int vertice)
    {
        if (vertice < VerticesCount)
        {
            foreach (var i in VerticeConnections[vertice])
            {
                VerticeConnections[i].Remove(vertice);
            }
            VerticeConnections.RemoveAt(vertice);
            return true;
        }
        return false;
    }

    public void UniteGraphs(IGraph graph)
    {
        var NewVerticesConnections = graph.GetVertices.Select(t => graph.GetAllConnectedVertices(t).ToList()).ToList();
        VerticeConnections.AddRange(NewVerticesConnections);
    }
}

public class WeightedGraphByListOfConnections : IWeightedGraph
{
    public WeightedGraphByListOfConnections(List<List<int>> listOfConnections, List<List<int>> listOfWeights)
    {
        VerticeConnections = listOfConnections;
        VerticeConnectionsWeights = listOfWeights;
    }

    public WeightedGraphByListOfConnections(IGraph graph)
    {
        UniteGraphs(graph);
    }

    public WeightedGraphByListOfConnections(IWeightedGraph graph)
    {
        UniteGraphs(graph);
    }

    private List<List<int>> VerticeConnections = new();
    private List<List<int>> VerticeConnectionsWeights = new();

    public int EdgesCount => VerticeConnections.Sum(t => t.Count);

    public HashSet<(int, int)> GetEdges
    {
        get
        {
            var Edges = new HashSet<(int, int)>();
            for (int i = 0; i < VerticesCount; i++)
            {
                foreach (var j in VerticeConnections[i])
                {
                    Edges.Add((i, j));
                }
            }
            return Edges;
        }
    }

    public IEnumerable<int> GetVertices
    {
        get
        {
            var arr = new int[VerticesCount];
            for (int i = 0; i < VerticesCount; i++)
            {
                arr[i] = i;
            }
            return arr;
        }
    }

    public int VerticesCount => VerticeConnections.Count;

    public bool AddEdge((int vertice1, int vertice2) Edge)
    {
        return AddWeightedEdge(Edge);
    }

    public bool AddVertice(params object[] Params)
    {
        foreach (int i in Params)
        {
            if (i > VerticesCount)
            {
                return false;
            }
        }
        foreach (int i in Params)
        {
            VerticeConnections[i].Add(VerticesCount);
        }
        VerticeConnections.Add(Params.Select(t => (int)t).ToList());
        return true;
    }

    public IEnumerable<int> GetAllConnectedVertices(int vertice)
    {
        return VerticeConnections[vertice];
    }

    public IEnumerable<(int, int)> GetAllIncidentEdges(int vertice)
    {
        return VerticeConnections[vertice].Select(t => (vertice, t));
    }

    public bool RemoveEdge((int vertice1, int vertice2) Edge)
    {
        if (VerticeConnections[Edge.vertice1].Remove(Edge.vertice2)) 
        {
            VerticeConnectionsWeights[Edge.vertice1].Remove(Edge.vertice2);
        }
        else
        {
            return false;
        }
        if (VerticeConnections[Edge.vertice2].Remove(Edge.vertice1)) 
        {
            VerticeConnectionsWeights[Edge.vertice2].Remove(Edge.vertice1);
        }
        else
        {
            VerticeConnections[Edge.vertice1].Add(Edge.vertice2);
            VerticeConnectionsWeights[Edge.vertice1].Add(Edge.vertice2);
            return false;
        }
        return true;
    }

    public bool RemoveVertice(int vertice)
    {
        if (vertice < VerticesCount)
        {
            foreach (var i in VerticeConnections[vertice])
            {
                VerticeConnections[i].Remove(vertice);
            }
            VerticeConnections.RemoveAt(vertice);
            return true;
        }
        return false;
    }

    public void UniteGraphs(IGraph graph)
    {
        var NewVerticesConnections = graph.GetVertices.Select(t => graph.GetAllConnectedVertices(t).ToList()).ToList();
        var NewVerticesConnectionsWeights = graph.GetVertices.Select(t => graph.GetAllConnectedVertices(t).Select(x => 1).ToList()).ToList();
        VerticeConnections.AddRange(NewVerticesConnections);
        VerticeConnectionsWeights.AddRange(NewVerticesConnectionsWeights);
    }

    public bool AddWeightedEdge((int vertice1, int vertice2) Edge, int Weight = 1)
    {
        if (Edge.vertice1 < VerticesCount && Edge.vertice2 < VerticesCount
            && !VerticeConnections[Edge.vertice1].Contains(Edge.vertice2))
        {
            VerticeConnections[Edge.vertice1].Add(Edge.vertice2);
            VerticeConnectionsWeights[Edge.vertice1].Add(Weight);
            return true;
        }
        return false;
    }

    public bool TryGetEdgeWeight((int vertice1, int vertice2) Edge, ref int weight)
    {
        if (Edge.vertice1 < VerticesCount)
        {
            var temp = VerticeConnections[Edge.vertice1].IndexOf(Edge.vertice2);
            if (temp != -1)
            { 
                weight = VerticeConnectionsWeights[Edge.vertice1][temp]; ;
                return true;
            }
        }
        return false;
    }

    public int GetEdgeWeight((int vertice1, int vertice2) Edge)
    {
        if (Edge.vertice1 < VerticesCount)
        {
            var temp = VerticeConnections[Edge.vertice1].IndexOf(Edge.vertice2);
            if (temp != -1)
            {
                return VerticeConnectionsWeights[Edge.vertice1][temp];
            }
        }
        return -1;
    }

    public void UniteGraphs(IWeightedGraph graph)
    {
        var NewVerticesConnections = graph.GetVertices.Select(t => graph.GetAllConnectedVertices(t).ToList()).ToList();
        var NewVerticesConnectionsWeights = graph.GetVertices.Select(t => graph.GetAllConnectedVertices(t)
            .Select(x => graph.GetEdgeWeight((t,x))).ToList()).ToList();
        VerticeConnections.AddRange(NewVerticesConnections);
        VerticeConnectionsWeights.AddRange(NewVerticesConnectionsWeights);
    }
}
