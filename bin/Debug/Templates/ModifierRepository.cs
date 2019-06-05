using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using R6.DataEngine.Common;
using R6.DataEngine.MapperConfig;
using R6.Repository.NeoProdRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R6.DataEngine.Modifier
{
    /// <summary>
    /// ModifierRepository
    /// </summary>
    /// <seealso cref="R6.DataEngine.Common.BaseRepository"/>
    /// <seealso cref="R6.DataEngine.Admin.ConstantsSetup.Modifier.IModifierRepository"/>
    public class ModifierRepository : BaseRepository, IModifierRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierRepository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ModifierRepository(ILogger<ModifierRepository> logger) : base(logger)
        {
        }

        /// <summary>
        /// Adds the modifier.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        public async Task<(int Id, string Message)> AddAsync(ModifierDto modifierDto)
        {
            int rowId = 0;
            string message = string.Empty;

            if (null != modifierDto)
            {
                var existisModifier = await GetByCodeAsync(modifierDto.Modifier_code.Trim());

                if (existisModifier != null)
                {
                    message = "The code entered is a duplicate.";
                }
                else
                {

                    var obj_C_PROCEDURE_MODIFIER = new C_PROCEDURE_MODIFIER();
                    obj_C_PROCEDURE_MODIFIER.Company_Id = CompanyId;
                    obj_C_PROCEDURE_MODIFIER.Created_By = UserId.ToString();
                    obj_C_PROCEDURE_MODIFIER.Created_Date = DateTime.Now;
                    obj_C_PROCEDURE_MODIFIER.Updated_By = UserId.ToString();
                    obj_C_PROCEDURE_MODIFIER.Updated_Date = DateTime.Now;

                    obj_C_PROCEDURE_MODIFIER.Proc_Mod_Code = modifierDto.Modifier_code.Trim();
                    obj_C_PROCEDURE_MODIFIER.Proc_Mod_Desc = modifierDto.Modifier_desc.Trim();
                    obj_C_PROCEDURE_MODIFIER.Is_Active = modifierDto.Is_active;
                    obj_C_PROCEDURE_MODIFIER.avail_chg_capture = false;

                    MidUnit.GetRepository<C_PROCEDURE_MODIFIER>()
                                   .Add(obj_C_PROCEDURE_MODIFIER);

                    var saved = await MidUnit.CommitAsync();

                    rowId = saved ? obj_C_PROCEDURE_MODIFIER.Procedure_Modifier_Id : 0;
                }
            }

            return (rowId, message);
        }

        /// <summary>
        /// Updates the modifier.
        /// </summary>
        /// <param name="modifierDto">The modifier dto.</param>
        /// <returns></returns>
        public async Task<(bool Response, string Message)> UpdateAsync(ModifierDto modifierDto)
        {
            bool response = false;
            string message = string.Empty;

            if (null != modifierDto)
            {
                var existisModifier = await GetByCodeAsync(modifierDto.Modifier_code.Trim());

                if (existisModifier != null && existisModifier.Procedure_Modifier_Id != modifierDto.Modifier_id)
                {
                    message = "The code entered is a duplicate.";
                }
                else
                {

                    var obj_C_PROCEDURE_MODIFIER = await MidUnit.GetRepository<C_PROCEDURE_MODIFIER>()
                                                            .GetByIdAsync(modifierDto.Modifier_id);

                    if (obj_C_PROCEDURE_MODIFIER == null)
                    {
                        message = Enums.ErrorCode.ObjectNotFound.ToString();
                    }
                    else
                    {
                        obj_C_PROCEDURE_MODIFIER.Updated_By = UserId.ToString();
                        obj_C_PROCEDURE_MODIFIER.Updated_Date = DateTime.Now;

                        obj_C_PROCEDURE_MODIFIER.Proc_Mod_Code = modifierDto.Modifier_code.Trim();
                        obj_C_PROCEDURE_MODIFIER.Proc_Mod_Desc = modifierDto.Modifier_desc.Trim();
                        obj_C_PROCEDURE_MODIFIER.Is_Active = modifierDto.Is_active;

                        MidUnit.GetRepository<C_PROCEDURE_MODIFIER>().Update(obj_C_PROCEDURE_MODIFIER);
                        response = await MidUnit.CommitAsync();
                    }
                }
            }
            return (response, message);
        }

        /// <summary>
        /// Gets the by identifier modifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<ModifierDto> GetByIdAsync(int id) =>
            MidUnit.GetRepository<C_PROCEDURE_MODIFIER>().GetAll()
                               .Where(x => x.Company_Id == this.CompanyId
                               && x.Procedure_Modifier_Id == id
                               )
                               .Select(o => new ModifierDto
                               {
                                   Modifier_id = o.Procedure_Modifier_Id,
                                   Modifier_code = o.Proc_Mod_Code,
                                   Modifier_desc = o.Proc_Mod_Desc,
                                   Is_active = o.Is_Active
                               }
                               ).FirstOrDefaultAsync();

        /// <summary>
        /// Searches the modifier.
        /// </summary>
        /// <param name="modifierCode">The modifier code.</param>
        /// <param name="modifierDesc">The modifier desc.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        public Task<List<ModifierDto>> SearchAsync(string modifierCode, string modifierDesc, bool showInactive) =>
            MidUnit.GetRepository<C_PROCEDURE_MODIFIER>().GetAll()
            .Where(x => x.Company_Id == CompanyId
            && x.Proc_Mod_Code.Contains(modifierCode ?? "")
            && x.Proc_Mod_Desc.Contains(modifierDesc ?? "")
            && (showInactive == true || x.Is_Active != showInactive))
            .Select(o => new ModifierDto
            {
                Modifier_id = o.Procedure_Modifier_Id,
                Modifier_code = o.Proc_Mod_Code,
                Modifier_desc = o.Proc_Mod_Desc,
                Is_active = o.Is_Active
            }
                               ).ToListAsync();

        /// <summary>
        /// Gets all active modifier.
        /// </summary>
        /// <returns></returns>
        public Task<List<ModifierDto>> GetAllAsync() =>
             MidUnit.GetRepository<C_PROCEDURE_MODIFIER>().GetAll()
            .Where(x => x.Is_Active == true && x.Company_Id == CompanyId)
            .Select(o => new ModifierDto
            {
                Modifier_id = o.Procedure_Modifier_Id,
                Modifier_code = o.Proc_Mod_Code,
                Modifier_desc = o.Proc_Mod_Desc,
                Is_active = o.Is_Active
            }).ToListAsync();



        /// <summary>
        /// Gets the by code modifier.
        /// </summary>
        /// <param name="modifierCode">The modifier code.</param>
        /// <returns></returns>
        private Task<C_PROCEDURE_MODIFIER> GetByCodeAsync(string modifierCode) =>
            MidUnit.GetRepository<C_PROCEDURE_MODIFIER>().GetAll().Where(m =>
                   m.Company_Id == CompanyId
                   && m.Proc_Mod_Code == modifierCode
                   ).FirstOrDefaultAsync();


    }
}