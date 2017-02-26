using Nidan.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidan.Document
{
    public class DocumentService : IDocumentService
    {
        public byte[] Bytes(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Guid Create(string category, string basePath, string documentCode, string studentName, string fileName, byte[] contents)
        {
            var newGuid = Guid.NewGuid();
            var categoryFileName = string.Concat(category, "_", newGuid, "_", fileName);
            var employeeDirectory = GetStudentDirectory(basePath, documentCode) ?? CreateEmployeeDirectory(basePath, studentName, documentCode);
            var categoryDirectory = Path.Combine(employeeDirectory, category);
            var filePath = Path.Combine(categoryDirectory, categoryFileName);
            var createdDateTime = DateTime.UtcNow;
            Directory.CreateDirectory(categoryDirectory);
            File.WriteAllBytes(filePath, contents);
            return newGuid;
        }

        private static string CreateStudentDirectory(string basePath, string studentName, string documentCode)
        {
            var directoryName = Path.Combine(basePath, CleanFilename(String.Format("{0}_{1}", studentName, documentCode)));
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
    }
}
