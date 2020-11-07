namespace Home.Core.Interfaces.Data.Actions
{
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a store that can fetch a specific (or all) record(s).
    /// </summary>
    /// <typeparam name="T">The Type of record</typeparam>
    public interface IFetchable<T>
    {
        /// <summary>
        /// Fetches all records of this type
        /// </summary>
        /// <returns>a collection of records</returns>
        Task<IQueryable<T>> Fetch();

        /// <summary>
        /// Fetches a single record by id
        /// </summary>
        /// <param name="id">the id of the record</param>
        /// <returns>record if found</returns>
        Task<T> Fetch(int id);

        /// <summary>
        /// Fetches a single record by id
        /// </summary>
        /// <param name="id">the id of the record</param>
        /// <returns>record if found</returns>
        Task<T> Fetch(ulong id);
    }
}
