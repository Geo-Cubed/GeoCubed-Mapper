namespace GeoCubed.Mapper;

/// <summary>
/// Mapping exception for throwing mapping related errors.
/// </summary>
public sealed class MappingException : Exception
{
    /// <summary>
    /// Gets or sets the sourceType of the mapping.
    /// </summary>
    public Type? FromType { get; set; }

    /// <summary>
    /// Gets or sets the destination type of the mapping.
    /// </summary>
    public Type? ToType { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingException"/> class.
    /// </summary>
    public MappingException()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingException"/> class.
    /// </summary>
    /// <param name="message">The error message to use.</param>
    public MappingException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingException"/> class.
    /// </summary>
    /// <param name="message">The error message to use.</param>
    /// <param name="innerException">The inner exception of the error.</param>
    public MappingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
