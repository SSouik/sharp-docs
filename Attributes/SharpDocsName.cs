using System;

namespace SharpDocs.Attributes
{
    /// <summary>
    /// API endpoint/resource display name in SharpDocs generated documentation
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = false)]
    public class SharpDocsName : Attribute
    {
        /// <summary>
        /// Name of API endpoint/resource
        /// </summary>
        public string Name { get; set; }

        public SharpDocsName() { }

        public SharpDocsName(string name)
        {
            Name = name;
        }
    }
}
