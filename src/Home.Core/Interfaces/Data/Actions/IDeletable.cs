namespace Home.Core.Interfaces.Data.Actions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a store that can delete records.
    /// </summary>
    /// <typeparam name="U">The type of the record's id</typeparam>
    public interface IDeletable<U>
    {
        /// <summary>
        /// Delete a single record.
        /// </summary>
        /// <param name="id">the id to delete</param>
        /// <returns>The deleted object?</returns>
        Task DeleteAsync(U id);
    }
}
