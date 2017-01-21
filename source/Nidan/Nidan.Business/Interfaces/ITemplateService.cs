namespace Nidan.Business.Interfaces
{
    public interface ITemplateService
    {
        byte[] CreatePDF(string jsonString, string templateName);
        byte[] CreatePDFfromPDFTemplate(System.Collections.Generic.Dictionary<string, string> formValues, string templateName);
        string CreateText(string jsonString, string templateName);
    }
}
