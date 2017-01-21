using System.Collections.Generic;
using Nidan.Business.Interfaces;
using Nidan.Business.TemplateServiceReference;

namespace Nidan.Business
{
    public class TemplateService : ITemplateService
    {
        private TemplateServiceV1100Client _service;

        public TemplateService()
        {
            _service = new TemplateServiceV1100Client();
        }

        public byte[] CreatePDF(string jsonString, string templateName)
        {
            if (templateName == null)
                throw new System.ArgumentNullException(nameof(templateName));

            return _service.CreatePDF(jsonString, templateName);
        }

        public byte[] CreatePDFfromPDFTemplate(Dictionary<string, string> formValues, string templateName)
        {
            return _service.CreatePDFfromPDFTemplate(formValues, templateName);
        }

        public string CreateText(string jsonString, string templateName)
        {
            return _service.CreateText(jsonString, templateName);
        }
    }
}
