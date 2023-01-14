namespace RushersGameEngine; 

public class DeltaTimeEventArgs : EventArgs {
    public double DeltaTime { get; }

    public DeltaTimeEventArgs(double deltaTime) {
        DeltaTime = deltaTime;
    }
}