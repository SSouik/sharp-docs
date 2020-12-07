using System.Collections.Generic;

namespace SharpDocs.Models
{
    /// <summary>
    /// SharpDocs representation of a Controller
    /// </summary>
    public record Controller
    {
        /// <summary>
        /// Name of the controller
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base route of the controller
        /// </summary>
        public string BaseRoute { get; set; }

        /// <summary>
        /// Collection of <see cref="ControllerMethod"/>
        /// </summary>
        public IEnumerable<ControllerMethod> Methods { get; set; }
    }
}
