namespace Badengine.Exceptions;

public class GameObjectNotRegisteredException : Exception {
    public GameObjectNotRegisteredException() : base("GameObject must be registered") { }
}