using System;
using System.Collections.Generic;
using System.IO;
using Nidan.Business.Interfaces;
using Nidan.Data.Interfaces;


namespace Nidan.Business
{
    public class TemplateService : ITemplateService
    {
        private readonly IPdfService _pdfService;
        private readonly IRazorService _razorService;
        private readonly INidanDataService _nidanDataService;

        public TemplateService(IRazorService razorService, IPdfService pdfService, INidanDataService nidanDataService)
        {
            _pdfService = pdfService;
            _razorService = razorService;
            _nidanDataService = nidanDataService;
        }

        public byte[] CreatePDF(int organisationId, string jsonString, string templateName)
        {
            try
            {
                string htmlData = CreateText(organisationId, jsonString, templateName);
                return _pdfService.CreatePDFfromHtml(htmlData);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public byte[] CreatePDFfromPDFTemplate(int organisationId, Dictionary<string, string> formValues, string templateName)
        {
            var templateDetails = _nidanDataService.RetrieveTemplateDetails(organisationId, templateName);
            return _pdfService.CreatePDFfromPDFTemplate(formValues, templateDetails.FilePath);
        }

        public string CreateText(int organisationId, string jsonString, string templateName)
        {
            if (!_razorService.IsTemplateCached(templateName))
            {
                var template = GetTemplateHtml(organisationId, templateName);
                _razorService.CacheTemplate(templateName, template);
            }
            return _razorService.CreateText(jsonString, templateName);
        }

        public string GetTemplateHtml(int organisationId, string templateName)
        {
            var templateDetails = _nidanDataService.RetrieveTemplateDetails(organisationId, templateName);
            return File.ReadAllText(templateDetails.FilePath);
        }
    }
}
