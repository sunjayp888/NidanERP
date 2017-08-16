using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using HiQPdf;
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

        public byte[] MergePDF(byte[] pdfFile1, byte[] pdfFile2)
        {
            // create an empty document which will become
            // the final document after merge
            var resultDocument = new PdfDocument();
            Stream stream1 = new MemoryStream(pdfFile1);
            // load the first document to be merged from a file
            var document1 = PdfDocument.FromStream(stream1);
            // load the second document to be merged from a FileStream
            Stream stream2 = new MemoryStream(pdfFile2);
            var document2 = PdfDocument.FromStream(stream2);
            // add the two documents to the result document
            resultDocument.AddDocument(document1);
            resultDocument.AddDocument(document2);
            try
            {
                byte[] pdfBuffer = resultDocument.WriteToMemory();
                return pdfBuffer;
            }
            finally
            {
                // close the result document
                resultDocument.Close();
                // close the merged documents
                // this will also close the pdfStream from which 
                // document 2 was loaded 
                document1.Close();
                document2.Close();
            }
        }
    }
}
