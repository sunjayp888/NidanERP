using System.Configuration;
using System.IO;
using RazorEngine.Templating;

namespace Nidan.Business.Interfaces
{
    public abstract class MvcTemplateBase<T> : TemplateBase<T>
    {
        public UrlHelper Url;

        public MvcTemplateBase()
        {
            Url = new UrlHelper();
        }
    }

    public class UrlHelper
    {
        public string Content(string content)
        {
            return Path.GetFullPath(Path.Combine(ConfigurationManager.AppSettings["TemplateRootFilePath"], content));
        }
    }
}