namespace Nidan.Business.Interfaces
{
    public interface ITemplateService
    {
        byte[] CreatePDF(int organisationId, string jsonString, string templateName);
        byte[] CreatePDFfromPDFTemplate(int organisationId, System.Collections.Generic.Dictionary<string, string> formValues, string templateName);
        string CreateText(int organisationId, string jsonString, string templateName);
    }
}
