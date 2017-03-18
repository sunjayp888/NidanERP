using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Nidan.Business.Interfaces
{
    public class RazorService : IRazorService
    {
        private IRazorEngineService _service;

        public RazorService()
        {
            var config = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(MvcTemplateBase<>),
                CachingProvider = new DefaultCachingProvider()          
            };

            _service = RazorEngineService.Create(config);            
            Engine.Razor = _service;            
        }

        public void CacheTemplate(string templateName, string template)
        {
            Engine.Razor.Compile(template, templateName);            
        }

        public bool IsTemplateCached(string templateName)
        {
            return Engine.Razor.IsTemplateCached(templateName, null);
        }

        public string CreateText(string jsonString, string templateName)
        {
            var data = JsonConvert.DeserializeObject<object>(jsonString);
            return Engine.Razor.Run(templateName, null, data);
        }
    }
}