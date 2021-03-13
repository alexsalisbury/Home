namespace Home.Core.Interfaces.Models
{
    using System.Collections.Generic;

    public interface IShyEntity
    {
        public static IEnumerable<IShyEntity> EmptyList => EmptyListCache;
        private static IEnumerable<IShyEntity> EmptyListCache = new List<IShyEntity>();

        int ShyId { get; }
    }
}
