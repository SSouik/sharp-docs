using System;

namespace SharpDocs.Attributes
{
    /// <summary>
    /// API endpoint/resource response description for SharpDocs generated documentation
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = true)]
    public sealed class SharpDocsResponse : Attribute
    {
        /// <summary>
        /// Status Code of API endpoint/resource response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Description of API endpoint/resource reponse
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of object that the API endpoint/resource returns
        /// </summary>
        public Type ResponseType { get; set; }

        public SharpDocsResponse() { }

        public SharpDocsResponse(int statusCode, string description, Type responseType)
        {
            StatusCode = statusCode;
            Description = description;
            ResponseType = responseType;
        }
    }
}
