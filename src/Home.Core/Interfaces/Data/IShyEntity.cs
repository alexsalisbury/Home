namespace Home.Core.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an entity in the ShyBot system.
    /// </summary>
    public interface IShyEntity
    {
        /// <summary>
        /// Returns a stable empty List<IShyEntity>
        /// </summary>
        public static IEnumerable<IShyEntity> EmptyList => EmptyListCache;

        /// <summary>
        /// A cached list object instance that is meant to stay encapulated and static.
        /// TODO: Lazy?
        /// </summary>
        private static IEnumerable<IShyEntity> EmptyListCache = new List<IShyEntity>();

        /// <summary>
        /// The identifier for this instance.
        /// </summary>
        int ShyId { get; }
    }
}
