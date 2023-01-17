namespace RushersGameEngine.Nodes; 

public class Node : IDisposable {
    public string? Name { get; set; }
    public Guid Guid { get; } = new();

    public Node? Parent {
        get => _parent;
        set {
            _parent?.RemoveNode(this);
            value?.AddNode(this);
            
            _parent = value;
        }
    }

    private readonly List<Node> _children = new();
    private Node? _parent;

    public event EventHandler? Update;
    public event EventHandler? Render;

    public void AddNode(Node? node) {
        _children.Add(node!);
    }

    public void RemoveNode(Node node) {
        _children.Remove(node);
    }

    public void RemoveNodes(Predicate<Node> predicate) {
        _children.RemoveAll(predicate);
    }

    public Node? GetNode(string name) {
        return _children.Find(node => node.Name == name);
    }

    public Node? GetNode(Guid guid) {
        return _children.Find(node => node.Guid == guid);
    }

    public Node? GetNode(Predicate<Node> predicate) {
        return _children.Find(predicate);
    }

    public Node? GetNode<T>() {
        return _children.Find(node => node is T);
    }

    public List<Node> GetNodes(Predicate<Node> predicate) {
        return _children.FindAll(predicate);
    }

    public List<Node> GetNodes() {
        return _children;
    }

    public virtual void Dispose() {
        Parent = null;

        foreach (var child in _children) {
            child.Dispose();
        }
    }

    internal void InvokeUpdate(double deltaTime) {
        Update?.Invoke(this, new DeltaTimeEventArgs(deltaTime));

        foreach (var child in _children) {
            child.InvokeUpdate(deltaTime);
        }
    }

    internal void InvokeRender(double deltaTime) {
        Render?.Invoke(this, new DeltaTimeEventArgs(deltaTime));

        foreach (var child in _children) {
            child.InvokeRender(deltaTime);
        }
    }
}