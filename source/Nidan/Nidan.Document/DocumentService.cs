using Nidan.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidan.Data;
using Nidan.Data.Interfaces;

namespace Nidan.Document
{
    public class DocumentService : IDocumentService
    {
        public readonly INidanDataService _nidanDataService;

        public DocumentService(INidanDataService nidanDataService)
        {
            _nidanDataService = nidanDataService;
        }
        public byte[] Bytes(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Guid Create(int organisationId, int centreId, int categoryId, string studentCode, string studentName, string description, string fileName, byte[] contents)
        {
            var newGuid = Guid.NewGuid();
            var category =
                _nidanDataService.RetrieveDocumentTypes(organisationId)
                    .FirstOrDefault(e => e.DocumentTypeId == categoryId);
            var centre = _nidanDataService.RetrieveCentre(organisationId, centreId, e => true);
            var basePath = CreateCentreBase(category.BasePath, centre.Name);
            var categoryFileName = string.Concat(category.Name, "_", newGuid, "_", fileName);
            var employeeDirectory = GetStudentDirectory(basePath, studentCode) ?? CreateStudentDirectory(basePath, studentName, studentCode);
            var categoryDirectory = Path.Combine(employeeDirectory, category.Name);
            var filePath = Path.Combine(categoryDirectory, categoryFileName);

            Directory.CreateDirectory(categoryDirectory);
            File.WriteAllBytes(filePath, contents);
            var document = new Entity.Document()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                DocumentTypeId = categoryId,
                StudentCode = studentCode,
                CreatedDateTime = DateTime.UtcNow.Date,
                Description = description,
                FileName = fileName,
                Location = filePath,
                StudentName = studentName,
                Guid = newGuid
            };

            _nidanDataService.Create<Entity.Document>(organisationId, document);
            return newGuid;
        }

        private static string CreateCentreBase(string basepath, string centreName)
        {
            if (!Directory.Exists(Path.Combine(basepath, centreName)))
                Directory.CreateDirectory(Path.Combine(basepath, centreName));
            return Path.Combine(basepath, centreName);
        }

        private static string CreateStudentDirectory(string basePath, string studentName, string studentCode)
        {
            var directoryName = Path.Combine(basePath, CleanFilename(String.Format("{0}_{1}", studentName, studentCode)));
            Directory.CreateDirectory(directoryName);
            return directoryName;
        }

        private static string CleanFilename(string filename)
        {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, chr) => current.Replace(chr, '_')).Replace(' ', '_');
        }

        private static string GetStudentDirectory(string basePath, string studentCode)
        {

            var employeeDirectories = Directory.GetDirectories(basePath, String.Format("*_{0}", studentCode));
            if (!employeeDirectories.Any())
                return null;
            if (employeeDirectories.Count() > 1)
                throw new Exception("Unable to identify employee");
            return employeeDirectories[0];
        }

        private static string CreateEmployeeDirectory(string basePath, string employeeName, string payrollId)
        {
            var directoryName = Path.Combine(basePath, CleanFilename(String.Format("{0}_{1}", employeeName, payrollId)));
            Directory.CreateDirectory(directoryName);
            return directoryName;
        }

        public byte[] GetDocumentBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public List<Entity.Document> RetrieveDocuments(int organisationId, int centreId, string category, string studentCode)
        {
            return _nidanDataService.RetrieveDocuments(organisationId, centreId, category, studentCode).ToList();
        }
    }
}
