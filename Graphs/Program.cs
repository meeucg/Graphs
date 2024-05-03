using System.Linq;
namespace GraphByList;

public class Program()
{
    static void Main(string[] args)
    {
        BenchmarkTools bt = new BenchmarkTools();

        bool[,] matrix =
        {
            {false,true,true,true,false},
            {true,false,false,false,true},
            {true,false,false,false,false},
            {true,false,false,false,true},
            {false,true,false,true,false}
        };

        bt.MemoryStart();
        GraphByMatrix graph1 = new(matrix);
        Console.WriteLine(bt.MemoryEnd());

        bt.MemoryStart();
        Console.WriteLine(bt.MemoryEnd());

        bt.MemoryStart();
        GraphByListOfConnections graph2 = new(new List<List<int>>
        {
            new List<int> {1,2,3},
            new List<int> {0, 4},
            new List<int> {0},
            new List<int> {0, 4},
            new List<int> {1, 3}
        });
        Console.WriteLine(bt.MemoryEnd());

        bt.MemoryStart();
        WeightedGraphByListOfConnections graph = new(new GraphByMatrix(matrix));
        Console.WriteLine(bt.MemoryEnd());

        GraphAlgorithms Run = new();
        Console.WriteLine("\n");

        Run.DijkstraWeighted(graph, 0); // хз почему, но первый раз всегда самый медленный

        bt.TimeStart();
        var n = Run.DijkstraWeighted(graph, 0);
        Console.WriteLine(bt.TimeEnd());
        foreach (var i in n)
        {
            Console.WriteLine(i);
        }

        bt.TimeStart();
        n = Run.DijkstraAllOnes(graph1, 0);
        Console.WriteLine(bt.TimeEnd());
        foreach (var i in n)
        {
            Console.WriteLine(i);
        }

        bt.TimeStart();
        n = Run.DijkstraAllOnes(graph2, 0);
        Console.WriteLine(bt.TimeEnd());
        foreach (var i in n)
        {
            Console.WriteLine(i);
        }
    }
}