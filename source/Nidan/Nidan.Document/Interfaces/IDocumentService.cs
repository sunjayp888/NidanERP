using System;
using System.Collections.Generic;

namespace Nidan.Document.Interfaces
{
    public interface IDocumentService
    {
        Guid Create(int organisationId, int centreId, int categoryId, string studentCode, string studentName,
            string description, string fileName, byte[] contents);

        byte[] GetDocumentBytes(string path);

        List<Entity.Document> RetrieveDocuments(int organisationId, int centreId, string category, string studentCode);
    }

    //public class Document
    //{
    //    string Type { get; set; }
    //    string Filename { get; set; }
    //    string DocumentCode { get; set; }
    //    byte[] bytes { get; set; }
    //    string Location { get; set; }
    //    string Description { get; set; }
    //    Guid DocumentGUID { get; set; }
    //}

    public class DocumentBytes
    {
        string FileName { get; set; }
        byte[] Bytes { get; set; }
    }
}
