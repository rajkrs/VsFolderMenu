using R6.DataEngine.Admin.ConstantsSetup.Modifier;
using R6.Enums;
using R6.Model.Common;
using R6.RuleEngine.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace R6.RuleEngine.Modifier
{
    /// <summary>
    /// Modifier Service 
    /// </summary>
    /// <seealso cref="R6.RuleEngine.Common.BaseService" />
    /// <seealso cref="R6.RuleEngine.Admin.ConstantSetup.Modifier.IModifierService" />
    public class ModifierService : BaseService,  IModifierService
    {
        /// <summary>
        /// The modifier repository
        /// </summary>
        private readonly IModifierRepository _modifierRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierService"/> class.
        /// </summary>
        /// <param name="modifierRepository">The modifier repository.</param>
        public ModifierService(IModifierRepository modifierRepository)
        {
            _modifierRepository = modifierRepository;
        }

        /// <summary>
        /// Sets the authorization profile.
        /// </summary>
        public override void SetAuthorizationProfile()
        {
            _modifierRepository.SetProfile(CompanyId, UserId, UserTypeR3Id, UserTypeR3Id);
        }


        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        public async Task<R6ResponseDto<int>> AddAsync(ModifierDto modifierDto)
        {
            var serviceResult = await _modifierRepository.AddAsync(modifierDto);
            if (string.IsNullOrEmpty(serviceResult.Message))
            {
                return GetResponse(serviceResult.Id);
            }
            else {
                return GetResponse(serviceResult.Id, ResponseStatus.Failure, new List<ErrorMessage>() { new ErrorMessage { Code =ErrorCode.Sql, Message = serviceResult.Message } });
            }

        }


        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        public async Task<R6ResponseDto<bool>> UpdateAsync(ModifierDto modifierDto) {
            var serviceResult = await _modifierRepository.UpdateAsync(modifierDto);
            if (string.IsNullOrEmpty(serviceResult.Message))
            {
                return GetResponse(serviceResult.Response);
            }
            else
            {
                return GetResponse(serviceResult.Response, ResponseStatus.Failure, new List<ErrorMessage>() { new ErrorMessage { Code = ErrorCode.Sql, Message = serviceResult.Message } });
            }
        }


        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<R6ResponseDto<ModifierDto>> GetByIdAsync(int id) =>
            GetResponse(await _modifierRepository.GetByIdAsync(id));

        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="modifierCode">The modifier code.</param>
        /// <param name="modifierDesc">The modifier desc.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        public async Task<R6ResponseDto<List<ModifierDto>>> SearchAsync(string modifierCode, string modifierDesc, bool showInactive) =>
           GetResponse(await _modifierRepository.SearchAsync(modifierCode, modifierDesc, showInactive));


        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<R6ResponseDto<List<ModifierDto>>> GetAllAsync() =>
                   GetResponse(await _modifierRepository.GetAllAsync());


    }
}
