using System.Collections.Generic;

namespace Nidan.Business.Interfaces
{

    public interface IPdfService
    {
        byte[] CreatePDFfromPDFTemplate(Dictionary<string, string> formValues, string templatePath);
        
        //html is already data bound, this service is not going to call the template
        byte[] CreatePDFfromHtml(string Html);  
    }
}
