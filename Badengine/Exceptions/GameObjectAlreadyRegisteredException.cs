namespace Badengine.Exceptions;

public class GameObjectAlreadyRegisteredException : Exception {
    public GameObjectAlreadyRegisteredException() : base("GameObject already registered") { }
}