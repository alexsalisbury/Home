namespace Home.Core.Interfaces.Data.Actions
{
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a store that can fetch a specific (or all) record(s).
    /// </summary>
    /// <typeparam name="T">The Type of record</typeparam>
    /// <typeparam name="U">The type of the record's id</typeparam>
    /// <remarks>Type "U" should be a IShyEntity since that is the root database type.</remarks>
    public interface IFetchable<T, U>
    {
        /// <summary>
        /// Fetches all records of this type
        /// </summary>
        /// <returns>a collection of records</returns>
        Task<IQueryable<T>> FetchAsync();

        /// <summary>
        /// Fetches a single record by id
        /// </summary>
        /// <param name="id">the id of the record</param>
        /// <returns>record if found</returns>
        Task<T> FetchAsync(U id);
    }
}
