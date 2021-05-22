namespace Home.Core.Interfaces.Models
{
    /// <summary>
    /// Represents an object that can be converted to a Dto.
    /// </summary>
    /// <typeparam name="T">The Dto Type</typeparam>
    public interface IHasDto<T>
    {
        /// <summary>
        /// Cache location if the Dto has already been calculated.
        /// </summary>
        T DtoCache { get; }

        /// <summary>
        /// Converts this object to its Dto
        /// </summary>
        /// <returns>The Dto record for this object type.</returns>
        T ToDto();
    }
}
