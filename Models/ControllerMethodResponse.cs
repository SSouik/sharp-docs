using System;

namespace SharpDocs.Models
{
    /// <summary>
    /// SharpDocs representation of a controller method response
    /// </summary>
    public record ControllerMethodResponse
    {
        /// <summary>
        /// Status code of the controller method response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Description of the controller method response
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of object of the controller method response
        /// </summary>
        public Type ResponseType { get; set; }
    }
}
