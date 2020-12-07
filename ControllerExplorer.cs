using System;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using SharpDocs.Models;
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
                .Where(HasHttpMethods)
                .Select(MapControllerInfo)
                .ToList();

            WriteSharpDocsFile(controllers);
        }

        /// <summary>
        /// Check if the type of object is derived from a controller
        /// </summary>
        /// <param name="type">Type of class</param>
        /// <returns>True if the class is a controller, false otherwise</returns>
        private static bool IsController(Type type)
            => typeof(ControllerBase).IsAssignableFrom(type);

        /// <summary>
        /// Check if the controller has any declared HTTP methods
        /// </summary>
        /// <param name="type">Instance of <see cref="ControllerBase"/></param>
        /// <returns>True if the controller has HTTP methods, false otherwise</returns>
        private static bool HasHttpMethods(Type type)
            => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(HasHttpMethodAttribute)
                .Any();

        /// <summary>
        /// Check if the <see cref="MethodInfo"/> contains a custom HTTP method attribute
        /// </summary>
        /// <param name="method">Instance of <see cref="MethodInfo"/> within an instance of a <see cref="ControllerBase"/></param>
        /// <returns>True if the method has a custom HTTP method attribute, false otherwise</returns>
        private static bool HasHttpMethodAttribute(MethodInfo method)
            => method.GetCustomAttributes()
                .Where(IsHttpAttribute)
                .Any();

        /// <summary>
        /// Validates that an <see cref="Attribute"/> is an instance of <see cref="HttpMethodAttribute"/>
        /// </summary>
        /// <param name="attribute">Custom attribute assigned to a controller method</param>
        /// <returns>True if the attribute is an instance of <see cref="HttpMethodAttribute"/>, false otherwise</returns>
        private static bool IsHttpAttribute(Attribute attribute)
            => typeof(HttpMethodAttribute).IsAssignableFrom(attribute.GetType());

#nullable enable
        /// <summary>
        /// Map controller data and methods to a <see cref="Controller"/>
        /// </summary>
        /// <param name="type">Type of class that is an instance of <see cref="ControllerBase"/></param>
        /// <returns><see cref="Controller"/> which is a SharpDocs representation of a controller</returns>
        private static Controller MapControllerInfo(Type type)
        {
            // Get the RouteAttribute on the controller if it exists
            RouteAttribute? routeAttribute = type.GetCustomAttribute(typeof(RouteAttribute)) as RouteAttribute;

            // Get all the public and declared methods within the controller and map them to a SharpDocs representation
            // of a controller method
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                .Select(methodInfo =>
                                {
                                    ControllerMethod controllerMethod = new ControllerMethod();

                                    methodInfo.GetCustomAttributes()
                                        .ToList()
                                        .ForEach(attribute =>
                                        {
                                            // Map each method attribute to its corresponding ControllerMethod property
                                            controllerMethod.Name = methodInfo.Name;

                                            if (attribute is HttpMethodAttribute httpMethod)
                                            {
                                                controllerMethod.Route = httpMethod.Template;
                                                controllerMethod.HttpMethods = controllerMethod.HttpMethods?.Concat(httpMethod.HttpMethods).ToArray() ?? httpMethod.HttpMethods.ToArray();
                                            }
                                            else if (attribute is RouteAttribute route)
                                                controllerMethod.Route = route.Template;
                                            else if (attribute is ConsumesAttribute consumes)
                                                controllerMethod.Consumes = consumes.ContentTypes.ToArray();
                                            else if (attribute is ProducesAttribute produces)
                                                controllerMethod.Produces = produces.ContentTypes.ToArray();
                                            else if (attribute is SharpDocsNameAttribute sharpDocsName)
                                                controllerMethod.SharpDocsName = sharpDocsName.Name;
                                            else if (attribute is SharpDocsDescriptionAttribute sharpDocsDescription)
                                            {
                                                controllerMethod.SharpDocsDescription = sharpDocsDescription.Description;
                                                controllerMethod.SharpDocsGroup = sharpDocsDescription.GroupName;
                                            }
                                            else if (attribute is SharpDocsResponseAttribute sharpDocsResponse)
                                            {
                                                controllerMethod.SharpDocsResponses = controllerMethod.SharpDocsResponses?
                                                                                        .Append(new ControllerMethodResponse
                                                                                        {
                                                                                            StatusCode = sharpDocsResponse.StatusCode,
                                                                                            Description = sharpDocsResponse.Description,
                                                                                            ResponseType = sharpDocsResponse.ResponseType
                                                                                        })
                                                                                        ??
                                                                                        new List<ControllerMethodResponse>
                                                                                        {
                                                                                            new ControllerMethodResponse
                                                                                            {
                                                                                                StatusCode = sharpDocsResponse.StatusCode,
                                                                                                Description = sharpDocsResponse.Description,
                                                                                                ResponseType = sharpDocsResponse.ResponseType
                                                                                            }
                                                                                        };
                                            }
                                        });

                                    return controllerMethod;
                                });

            // Build the SharpDocs representation of a controller
            Controller controller = new Controller
            {
                Name = type.Name,
                BaseRoute = routeAttribute?.Template,
                Methods = methods
            };

            return controller;
        }
#nullable restore

        /// <summary>
        /// Write the controller information to a local JSON file
        /// </summary>
        /// <param name="controllers">List of <see cref="Controller"/> to write to JSON file</param>
        private static void WriteSharpDocsFile(List<Controller> controllers)
        {
            // Create a file in the root of the project
            string path = @"./SharpDocs.json";

            using (FileStream fileStream = File.Create(path))
            {
                byte[] bytes = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(controllers));
                File.SetAttributes(path, FileAttributes.Normal);

                // Add some information to the file.
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
