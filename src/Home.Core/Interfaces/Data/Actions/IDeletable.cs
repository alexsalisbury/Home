namespace Home.Core.Interfaces.Data.Actions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a store that can delete records.
    /// </summary>
    /// <typeparam name="T">The type of record</typeparam>
    public interface IDeletable<T>
    {
        /// <summary>
        /// Delete a single record.
        /// </summary>
        /// <param name="id">the id to delete</param>
        /// <returns>The deleted object?</returns>
        Task<T> Delete(int id);
    }
}
