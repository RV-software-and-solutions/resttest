namespace RestTest.Core.Services.Graph.Models;
public abstract class AbstractVertex<T>
{
    public T Value { get; set; }
    public List<AbstractVertex<T>> Adjacent { get; set; }
    public bool Visited { get; set; }

    public virtual AbstractVertex<T> InitVertex(T value)
    {
        Value = value;
        Adjacent = new List<AbstractVertex<T>>();
        Visited = false;
        return this;
    }

    public virtual void AddEdge(AbstractVertex<T> vertex)
    {
        Adjacent.Add(vertex);
    }
}
