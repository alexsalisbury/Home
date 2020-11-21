namespace Home.Core.Interfaces.Data.Actions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a store that can update a record.
    /// </summary>
    /// <typeparam name="T">The type of record</typeparam>
    public interface IPatchable<T, U>
    {
        /// <summary>
        /// update the record with the id (if present) to match the passed object
        /// </summary>
        /// <param name="id">The record to update</param>
        /// <param name="patch">The object data</param>
        /// <returns>The updated object</returns>
        Task<T> Patch(U id, T patch);
    }
}
