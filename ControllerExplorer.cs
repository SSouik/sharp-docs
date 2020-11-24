using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SharpDocs.Attributes;

namespace SharpDocs
{
    public static class ControllerExplorer
    {
        /// <summary>
        /// Add SharpDocs support to the current web service
        /// </summary>
        /// <param name="services">IServiceCollection of the current service</param>
        public static void AddSharpDocs(this IServiceCollection services)
        {
            var controllers = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(IsController)
                .SelectMany(MapMethods)
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = x.GetCustomAttributes().ToList() })
                .ToList();

            foreach (var controller in controllers)
            {
                foreach (var attribute in controller.Attributes)
                {
                    if (attribute is SharpDocsName)
                    {
                        var attr = attribute as SharpDocsName;
                        Console.WriteLine($"SharpDocs Name: {attr.Name}");
                    }
                    else if (attribute is SharpDocsDescription)
                    {
                        var attr = attribute as SharpDocsDescription;
                        Console.WriteLine($"SharpDocs Description: {attr.Description}");
                        Console.WriteLine($"SharpDocs Group Name: {attr.GroupName}");
                    }
                    else if (attribute is SharpDocsResponse)
                    {
                        var attr = attribute as SharpDocsResponse;
                        Console.WriteLine($"SharpDocs Status Code: {attr.StatusCode}");
                        Console.WriteLine($"SharpDocs Group Description: {attr.Description}");
                        Console.WriteLine($"SharpDocs Type: {attr.ResponseType.Name}");
                    }
                }
            }
        }

        /// <summary>
        /// Check if the type of object is derived from a controller
        /// </summary>
        /// <param name="type">Type of class</param>
        /// <returns>True if the class is a controller, false otherwise</returns>
        private static bool IsController(Type type)
            => typeof(ControllerBase).IsAssignableFrom(type);

        /// <summary>
        /// Map controller methods to a consolidated list
        /// </summary>
        /// <param name="type">Type of class</param>
        /// <returns>Enummerable of public methods bound to the class</returns>
        private static IEnumerable<MethodInfo> MapMethods(Type type)
            => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
    }
}
