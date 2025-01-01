namespace GeoCubed.Mapper.Common;

internal class MappingExceptionBuilder
{
    private Type? _fromType;
    private Type? _toType;
    private string _message;
    private Exception? _innerException;

    private MappingExceptionBuilder()
    {
        this._message = string.Empty;
    }

    internal static MappingExceptionBuilder AMappingException()
    {
        return new MappingExceptionBuilder();
    }

    internal MappingExceptionBuilder WithMessage(string message)
    {
        this._message = message;
        return this;
    }

    internal MappingExceptionBuilder WithFromType(Type from)
    {
        this._fromType = from;
        return this;
    }

    internal MappingExceptionBuilder WithToType(Type to)
    {
        this._toType = to;
        return this;
    }

    internal MappingExceptionBuilder WithInnerException(Exception innerException)
    {
        this._innerException = innerException;
        return this;
    }

    internal MappingException Build()
    {
        MappingException ex;
        if (string.IsNullOrEmpty(this._message))
        {
            ex = new MappingException();
        }
        else if (this._innerException == null)
        { 
            ex = new MappingException(this._message);
        }
        else
        {
            ex = new MappingException(this._message, this._innerException);
        }

        ex.FromType = this._fromType;
        ex.ToType = this._toType;

        return ex;
    }
}
