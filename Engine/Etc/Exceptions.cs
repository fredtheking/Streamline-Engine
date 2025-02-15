namespace StreamlineEngine.Engine.Etc;

public class NotOverriddenException(string? message = null) : Exception(message);
public class CallNotAllowedException(string? message = null) : Exception(message);
public class NotInitialisedException(string? message = null) : Exception(message);