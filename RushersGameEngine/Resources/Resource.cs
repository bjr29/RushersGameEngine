namespace RushersGameEngine.Resources;

public class Resource : IDisposable {
    protected Resource() {
        Engine.Resources.Add(this);
    }

    public virtual void Dispose() { }
}