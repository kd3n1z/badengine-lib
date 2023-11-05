namespace Badengine.Exceptions;

public class ComponentAlreadyRegisteredException : Exception {
    public ComponentAlreadyRegisteredException() : base("Component already registered") { }
}