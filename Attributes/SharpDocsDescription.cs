using System;

namespace SharpDocs.Attributes
{
    /// <summary>
    /// API endpoint/resource description and group name in SharpDocs generated documentation
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = false)]
    public sealed class SharpDocsDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Description of API endpoint/resource
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Group name of API endpoint/resource
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="SharpDocsDescriptionAttribute"/>
        /// </summary>
        public SharpDocsDescriptionAttribute() { }

        /// <summary>
        /// Create a new instance of <see cref="SharpDocsDescriptionAttribute"/>
        /// </summary>
        /// <param name="description">Description of API endpoint/resource</param>
        /// <param name="groupName">Group name of API endpoint/resource</param>
        public SharpDocsDescriptionAttribute(string description, string groupName)
        {
            Description = description;
            GroupName = groupName;
        }
    }
}
