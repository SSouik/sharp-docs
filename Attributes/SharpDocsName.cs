using System;

namespace SharpDocs.Attributes
{
    /// <summary>
    /// API endpoint/resource display name in SharpDocs generated documentation
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = false)]
    public class SharpDocsNameAttribute : Attribute
    {
        /// <summary>
        /// Name of API endpoint/resource
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="SharpDocsNameAttribute"/>
        /// </summary>
        public SharpDocsNameAttribute() { }

        /// <summary>
        /// Create a new instance of <see cref="SharpDocsNameAttribute"/>
        /// </summary>
        /// <param name="name">Name of API endpoint/resource</param>
        public SharpDocsNameAttribute(string name)
        {
            Name = name;
        }
    }
}
