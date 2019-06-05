using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using R6.Model.Common;
using R6.RuleEngine.Modifier;
using R6.Service.Common;

namespace R6.Service.Controllers
{

    /// <summary>
    /// Modifier
    /// </summary>
    /// <seealso cref="R6.Service.Common.BaseApiController" />
    [Route("api/modifier")]
    [ApiController]
    public class ModifierController : BaseApiController
    {

        #region Private members
        /// <summary>
        /// The modifier service
        /// </summary>
        private readonly IModifierService _modifierService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierController"/> class.
        /// </summary>
        /// <param name="modifierService">The modifier service.</param>
        public ModifierController(IModifierService modifierService)
        {
            _modifierService = modifierService;
            _modifierService.CompanyId = CompanyId;
            _modifierService.UserId = UserId;
        }

        /// <summary>
        /// Searches the modifier.
        /// </summary>
        /// <param name="modifier_code">The modifier code.</param>
        /// <param name="modifier_desc">The modifier desc.</param>
        /// <param name="show_inactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        [HttpGet("search")]
        [Produces(typeof(R6ResponseDto<List<ModifierDto>>))]
        public Task<R6ResponseDto<List<ModifierDto>>> SearchAsync(string modifier_code, string modifier_desc, bool show_inactive) =>
            _modifierService.SearchAsync(modifier_code, modifier_desc, show_inactive);


        /// <summary>
        /// Adds the modifier.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(R6ResponseDto<int>))]

        public Task<R6ResponseDto<int>> AddAsync(ModifierDto modifierDto) =>
             _modifierService.AddAsync(modifierDto);



        /// <summary>
        /// Updates the modifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces(typeof(R6ResponseDto<bool>))]
        public Task<R6ResponseDto<bool>> UpdateAsync(int id, ModifierDto modifierDto)
        {
            modifierDto.Modifier_id = id;
            return _modifierService.UpdateAsync(modifierDto);
        }

        /// <summary>
        /// Gets the modifier by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(typeof(R6ResponseDto<ModifierDto>))]
        public Task<R6ResponseDto<ModifierDto>> GetByIdAsync(int id) =>
            _modifierService.GetByIdAsync(id);


        /// <summary>
        /// Gets all modifiers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(R6ResponseDto<List<ModifierDto>>))]
        public Task<R6ResponseDto<List<ModifierDto>>> GetAllAsync() =>
            _modifierService.GetAllAsync();





    }
}
