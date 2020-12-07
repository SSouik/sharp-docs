using System;

namespace SharpDocs.Attributes
{
    /// <summary>
    /// API endpoint/resource response description for SharpDocs generated documentation
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = true)]
    public sealed class SharpDocsResponseAttribute : Attribute
    {
        /// <summary>
        /// Status Code of API endpoint/resource response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Description of API endpoint/resource response
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of object that the API endpoint/resource returns
        /// </summary>
        public Type ResponseType { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="SharpDocsResponseAttribute"/>
        /// </summary>
        public SharpDocsResponseAttribute() { }

        /// <summary>
        /// Create a new instance of <see cref="SharpDocsResponseAttribute"/>
        /// </summary>
        /// <param name="statusCode">Status Code of API endpoint/resource response</param>
        /// <param name="description">Description of API endpoint/resource response</param>
        /// <param name="responseType">Type of object that the API endpoint/resource returns</param>
        public SharpDocsResponseAttribute(int statusCode, string description, Type responseType)
        {
            StatusCode = statusCode;
            Description = description;
            ResponseType = responseType;
        }
    }
}
