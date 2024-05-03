
namespace GraphByList;

public class GraphByMatrix : IGraph
{
    bool[,] Matrix = {};
    int size = 0;

    public GraphByMatrix(bool[,] matrix)
    {
        Matrix = matrix;
        size = matrix.GetLength(0);
    }

    public GraphByMatrix(IGraph graph)
    {
        UniteGraphs(graph);
    }

    public int EdgesCount
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < VerticesCount; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (Matrix[i, j])
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
    }

    public HashSet<(int, int)> GetEdges
    {
        get 
        {
            var temp = new HashSet<(int, int)> ();
            for (int i = 0; i < VerticesCount; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (Matrix[i, j])
                    {
                        temp.Add((i,j));
                    }
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

    public int VerticesCount => size;

    public bool AddEdge((int vertice1, int vertice2) Edge)
    {
        if (Edge.vertice1 >= VerticesCount || Edge.vertice2 >= VerticesCount)
        {
            return false;
        }
        Matrix[Edge.vertice1, Edge.vertice2] = true;
        Matrix[Edge.vertice2, Edge.vertice1] = true;
        return true;
    }

    public bool AddVertice(params object[] Params)
    {
        var temp = new bool[VerticesCount + 1, VerticesCount + 1];
        foreach (int i in Params)
        { 
            if(i >= VerticesCount)
            {
                return false;
            }
            temp[VerticesCount, i] = true;
            temp[i, VerticesCount] = true;
        }
        
        for (int i = 0; i < VerticesCount; i++) 
        {
            for (int j = 0; j < VerticesCount; j++)
            {
                temp[i, j] = Matrix[i, j];
            }
        }
        size++;
        Matrix = temp;
        return true;
    }

    public IEnumerable<int> GetAllConnectedVertices(int vertice)
    {
        var temp = new List<int>();
        for (int i = 0; i < VerticesCount; i++)
        {
            if (Matrix[vertice, i])
            { 
                temp.Add(i); 
            }
        }
        return temp;
    }

    public IEnumerable<(int, int)> GetAllIncidentEdges(int vertice)
    {
        return (IEnumerable<(int,int)>)GetAllIncidentEdges(vertice).Select(t => (vertice, t)).ToList();
    }

    public bool RemoveEdge((int vertice1, int vertice2) Edge)
    {
        if (Edge.vertice1 >= VerticesCount || Edge.vertice2 >= VerticesCount)
        {
            return false;
        }
        Matrix[Edge.vertice1, Edge.vertice2] = false;
        Matrix[Edge.vertice2, Edge.vertice1] = false;
        return true;
    }

    public bool RemoveVertice(int vertice)
    {
        var temp = new bool[VerticesCount + 1, VerticesCount + 1];
        for (int i = 0; i < VerticesCount; i++)
        {
            for (int j = 0; j < VerticesCount; j++)
            {
                temp[i, j] = Matrix[i, j];
            }
        }
        size++;
        Matrix = temp;
        return false;
    }

    public void UniteGraphs(IGraph graph)
    {
        var res = new bool[graph.VerticesCount + VerticesCount, graph.VerticesCount + VerticesCount];
        foreach (int i in graph.GetVertices)
        {
            foreach (int j in graph.GetAllConnectedVertices(i))
            {
                res[i + VerticesCount, j + VerticesCount] = true;
            }
        }
        for (int i = 0; i < VerticesCount; i++)
        {
            for (int j = 0; j < VerticesCount; j++)
            {
                res[i,j] = Matrix[i,j];
            }
        }
    }
}