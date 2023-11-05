namespace Badengine.Exceptions;

public class ComponentIsNonRegistrableException : Exception {
    public ComponentIsNonRegistrableException() : base("Component is non registrable") { }
}