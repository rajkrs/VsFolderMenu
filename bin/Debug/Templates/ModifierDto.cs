using System.ComponentModel.DataAnnotations;

namespace R6.Model.Modifier
{
    /// <summary>
    /// Eqe of Procedure Modifier
    /// </summary>
    public class ModifierDto
    {
        /// <summary>
        /// Gets or sets the modifier identifier.
        /// </summary>
        /// <value>
        /// The modifier identifier.
        /// </value>
        public int Modifier_id { get; set; }
        /// <summary>
        /// Gets or sets the modifier code.
        /// </summary>
        /// <value>
        /// The modifier code.
        /// </value>
        [Required]
        public string Modifier_code { get; set; }
        /// <summary>
        /// Gets or sets the modifier desc.
        /// </summary>
        /// <value>
        /// The modifier desc.
        /// </value>
        public string Modifier_desc { get; set; }
        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool? Is_active { get; set; }
    }
}
