using System.Collections.Generic;
using System.IO;
using HiQPdf;
using iTextSharp.text.pdf;

namespace Nidan.Business.Interfaces
{
    public class PdfService : IPdfService
    {
        public byte[] CreatePDFfromHtml(string html)
        {
            //var htmlpdf = new HtmlToPdf();// { }//SerialNumber = "sfnY4OHV1/3Y08PQw8iEn4GRgJGAkYWCgZGCgJ+Ag5+IiIiI" };
            var htmlpdf = new HtmlToPdf { };
            htmlpdf.Document.Margins = new PdfMargins(20, 20, 20, 10);
            using (var result = new MemoryStream())
            {
                htmlpdf.ConvertHtmlToStream(html, null, result);
                return result.ToArray();
            }
        }

        public byte[] CreatePDFfromPDFTemplate(Dictionary<string, string> formValues, string templatePath)
        {
            var reader = new PdfReader(templatePath);
            var outputPdfStream = new MemoryStream();
            var pdfStamper = new PdfStamper(reader, outputPdfStream)
            {
                FormFlattening = true,
                FreeTextFlattening = true
            };
            var pdfFormFields = pdfStamper.AcroFields;
            foreach (var fieldName in formValues)
            {
                pdfFormFields.SetField(fieldName.Key, fieldName.Value);
            }
            pdfStamper.Close();
            return outputPdfStream.ToArray();
        }

    }
}