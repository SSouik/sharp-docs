using System.Collections.Generic;

namespace SharpDocs.Models
{
    /// <summary>
    /// SharpDocs representation of a Controller Method
    /// </summary>
    public record ControllerMethod
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Array of HTTP methods attached to the method
        /// </summary>
        public string[] HttpMethods { get; set; }

        /// <summary>
        /// Route of the method
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Array of HTTP Accept headers attached to the method
        /// </summary>
        public string[] Consumes { get; set; }

        /// <summary>
        /// Array of content produced by the method
        /// </summary>
        public string[] Produces { get; set; }

        /// <summary>
        /// Name of the method in SharpDocs documentation
        /// </summary>
        public string SharpDocsName { get; set; }

        /// <summary>
        /// Description of the method in SharpDocs documentation
        /// </summary>
        public string SharpDocsDescription { get; set; }

        /// <summary>
        /// Group of the method in SharpDocs documentation
        /// </summary>
        public string SharpDocsGroup { get; set; }

        /// <summary>
        /// Collection of <see cref="ControllerMethodResponse"/> that represents an API endpoint/resource reponse in SharpDocs documentation
        /// </summary>
        public IEnumerable<ControllerMethodResponse> SharpDocsResponses { get; set; }
    }
}
