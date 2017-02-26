using System;

namespace Nidan.Document.Interfaces
{
    public interface IDocumentService
    {
        Guid Create(string category, string basePath, string documentCode, string studentName, string fileName, byte[] contents);
        byte[] GetDocumentBytes(string path);
    }

    public class Document
    {
        string Type { get; set; }
        string Filename { get; set; }
        string DocumentCode { get; set; }
        byte[] bytes { get; set; }
        string Location { get; set; }
        string Description { get; set; }
        Guid DocumentGUID { get; set; }
    }

    public class DocumentBytes
    {
        string FileName { get; set; }
        byte[] Bytes { get; set; }
    }
}
