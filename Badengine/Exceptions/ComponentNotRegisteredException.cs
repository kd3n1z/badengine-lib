namespace Badengine.Exceptions;

public class ComponentNotRegisteredException : Exception {
    public ComponentNotRegisteredException() : base("Component must be registered") { }
}