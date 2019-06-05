using R6.Model.Common;
using R6.RuleEngine.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace R6.RuleEngine.Modifier
{
    /// <summary>
    /// IModifierService
    /// </summary>
    /// <seealso cref="R6.RuleEngine.Common.IAuthorizeProfile" />
    public interface IModifierService : IAuthorizeProfile
    {
        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="modifierCode">The modifier code.</param>
        /// <param name="modifierDesc">The modifier desc.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        Task<R6ResponseDto<List<ModifierDto>>> SearchAsync(string modifierCode, string modifierDesc, bool showInactive);
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        Task<R6ResponseDto<int>> AddAsync(ModifierDto modifierDto);
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        Task<R6ResponseDto<bool>> UpdateAsync(ModifierDto modifierDto);
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<R6ResponseDto<ModifierDto>> GetByIdAsync(int id);

        /// <summary>
        /// Gets all Modifiers.
        /// </summary>
        /// <returns></returns>
        Task<R6ResponseDto<List<ModifierDto>>> GetAllAsync();
    }
}
