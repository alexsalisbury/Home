namespace Home.Core.Interfaces.Data.Actions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a store that can ensure a record exists preventing double creates.
    /// </summary>
    /// <typeparam name="T">The type of record</typeparam>
    public interface IEnsurable<T>
    {
        /// <summary>
        /// Ensure a single record exists.
        /// </summary>
        /// <param name="id">the record to match or write.</param>
        /// <returns>true if found or created.</returns>
        Task<bool> EnsureAsync(T record);
    }
}
