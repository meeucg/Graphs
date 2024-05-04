
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
            Edges[Edge.vertice2].Add(Edge.vertice1);
            return true;
        }
        return false;
    }

    public bool AddVertice(params object[] Params)
    {
        Vertices.Add(EdgesCount);
        if (Params.All(t => (int)t <= VerticesCount))
        {
            Edges.Add(Params.Select(t => (int)t).ToList());
            return true;
        }
        else
        {
            Vertices.RemoveAt(VerticesCount-1);
        }
        return false;
    }

    public IEnumerable<int> GetAllConnectedVertices(int vertice)
    {
        return Edges[Vertices[vertice]];
    }

    public IEnumerable<(int, int)> GetAllIncidentEdges(int vertice)
    {
        return Edges[Vertices[vertice]].Select(t=>(vertice, t));
    }

    public bool RemoveEdge((int vertice1, int vertice2) Edge)
    {
        return Edges[Vertices[Edge.vertice1]].Remove(Edge.vertice2) 
            && Edges[Vertices[Edge.vertice2]].Remove(Edge.vertice1);
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
