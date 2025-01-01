/// <summary>
/// Mapping interface to be using to specify a mapping between two classes.
/// </summary>
/// <typeparam name="From">The input type.</typeparam>
/// <typeparam name="To">The output type.</typeparam>
public interface IMapping<in From, out To>
    where From : class
    where To : class 
{
    /// <summary>
    /// Map the in object to an instance of the out object.
    /// </summary>
    /// <param name="obj">The input object.</param>
    /// <returns>The output object.</returns>
    To Map(From obj);
}