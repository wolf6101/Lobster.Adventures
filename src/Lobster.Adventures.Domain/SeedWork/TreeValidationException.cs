[System.Serializable]
public class TreeValidationException : System.Exception
{
    public TreeValidationException() { }
    public TreeValidationException(string message) : base(message) { }
    public TreeValidationException(string message, string guardName) : base(message) {
        GuardName = guardName;
    }
    public TreeValidationException(string message, System.Exception inner) : base(message, inner) { }
    protected TreeValidationException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    public string GuardName { get; }
}