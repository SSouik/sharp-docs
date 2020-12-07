using System.Collections.Generic;

namespace SharpDocs.Models
{
    public record Controller
    {
        public string Name { get; set; }
        public string BaseRoute { get; set; }
        public IEnumerable<ControllerMethod> Methods { get; set; }
    }
}
