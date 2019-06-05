using R6.DataEngine.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace R6.DataEngine.Modifier
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="R6.DataEngine.Common.IAuthorizeProfile" />
    public interface IModifierRepository : IAuthorizeProfile
    {
        /// <summary>
        /// Searches the Modifiers.
        /// </summary>
        /// <param name="modifierCode">The modifier code.</param>
        /// <param name="modifierDesc">The modifier desc.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        Task<List<ModifierDto>> SearchAsync(string modifierCode, string modifierDesc, bool showInactive);
        /// <summary>
        /// Adds the Modifier.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        Task<(int Id, string Message)> AddAsync(ModifierDto modifierDto);
        /// <summary>
        /// Updates the Modifier.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        Task<(bool Response, string Message)> UpdateAsync(ModifierDto modifierDto);
        /// <summary>
        /// Gets the by identifier Modifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ModifierDto> GetByIdAsync(int id);
        /// <summary>
        /// Gets all Modifiers.
        /// </summary>
        /// <returns></returns>
        Task<List<ModifierDto>> GetAllAsync();

    }
}
