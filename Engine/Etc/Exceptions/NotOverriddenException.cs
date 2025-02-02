namespace StreamlineEngine.Engine.Etc.Exceptions;

public class NotOverriddenException(string? message = null) : Exception(message);
public class CallNotAllowedException(string? message = null) : Exception(message);