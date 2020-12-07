using System;

namespace SharpDocs.Models
{
    public record ControllerMethod
    {
        public string Name { get; set; }
        public string[] HttpMethods { get; set; }
        public string Route { get; set; }
        public string[] Consumes { get; set; }
        public string[] Produces { get; set; }
        public string SharpDocsName { get; set; }
        public string SharpDocsDescription { get; set; }
        public string SharpDocsGroup { get; set; }
        public int SharpDocsStatusCode { get; set; }
        public string SharpDocsResponseDescription { get; set; }
        public Type SharpDocsRespnseType { get; set; }
    }
}
