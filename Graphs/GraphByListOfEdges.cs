
namespace GraphByList;

public class GraphByListOfEdges : IGraph
{
    public List<List<int>> Edges { get; set; }
    public List<int> Vertices { get; set; }

    public int EdgesCount => Edges.Sum(t => t.Count);

    public HashSet<(int, int)> GetEdges
    {
        get
        { 
            var temp = new HashSet<(int, int)> ();
            foreach (var vertice in Vertices) 
            {
                foreach (var edge in Edges[vertice])
                {
                    temp.Add((vertice, edge));
                }
            }
            return temp;
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

    public int VerticesCount => Vertices.Count;

    public bool AddEdge((int vertice1, int vertice2) Edge)
    {
        if (Edge.vertice1 < VerticesCount && Edge.vertice2 < VerticesCount)
        {
            Edges[Edge.vertice1].Add(Edge.vertice2);
            return true;
        }
        return false;
    }

    public bool AddVertice(params object[] Params)
    {
        /*Vertices.Add(VerticesCount);
        foreach (int vertice in Params)
        {
            if (vertice > VerticesCount)
            { 
                
            }
        }*/
        throw new NotImplementedException ();
    }

    public IEnumerable<int> GetAllConnectedVertices(int vertice)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int, int)> GetAllIncidentEdges(int vertice)
    {
        throw new NotImplementedException();
    }

    public bool RemoveEdge((int vertice1, int vertice2) Edge)
    {
        throw new NotImplementedException();
    }

    public bool RemoveVertice(int vertice)
    {
        throw new NotImplementedException();
    }

    public void UniteGraphs(IGraph graph)
    {
        throw new NotImplementedException();
    }
}
