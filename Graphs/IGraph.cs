public interface IGraph
{
    int EdgesCount { get; } //кол-во рёбер
    HashSet<(int, int)> GetEdges { get; } //список рёбер
    
    IEnumerable<int> GetVertices { get; } //список вершин
    int VerticesCount { get; } //кол-во вершин

    bool AddVertice(params object[] Params); //добавить вершину
    bool AddEdge((int vertice1, int vertice2) Edge); //добавить ребро

    IEnumerable<int> GetAllConnectedVertices(int vertice); //список всех смежных вершин
    IEnumerable<(int, int)> GetAllIncidentEdges(int vertice); //список всех инцидентных врешине ребер в формате (вершина1, вершина2)
    
    bool RemoveEdge((int vertice1, int vertice2) Edge); //удалить ребро
    bool RemoveVertice(int vertice); //удалить вершину

    void UniteGraphs(IGraph graph); //объединить два графа, считая что множества их вершин не пересекаются
}


public interface IOrientedGraph : IGraph
{
    IEnumerable<(int, int)> GetAllIngoingEdges(int vertice); //все входящие ребра
    IEnumerable<(int, int)> GetAllOutgoingEdges(int vertice); //все выходящие ребра 
}


public interface IWeightedGraph : IGraph
{
    bool AddWeightedEdge((int vertice1, int vertice2) Edge, int Weight = 1); //добавление ребра с весом
    int GetEdgeWeight((int vertice1, int vertice2) Edge);
    bool TryGetEdgeWeight((int vertice1, int vertice2) Edge, ref int Weight);
    void UniteGraphs(IWeightedGraph graph);
}