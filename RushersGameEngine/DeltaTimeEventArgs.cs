namespace RushersGameEngine; 

public class DeltaTimeEventArgs : System.EventArgs {
    public double DeltaTime { get; }

    public DeltaTimeEventArgs(double deltaTime) {
        DeltaTime = deltaTime;
    }
}