using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Nidan.Models
{
    public class DocumentViewModel
    {
        public byte[] DocumentBytes
        {
            get
            {
                if (Attachment != null)
                {
                    MemoryStream target = new MemoryStream();
                    Attachment.InputStream.CopyTo(target);
                    byte[] bytes = target.ToArray();
                    return bytes;
                }
                else if (!string.IsNullOrEmpty(DocumentBytesString))
                {
                    return Convert.FromBase64String(DocumentBytesString);
                }
                else
                {
                    return new byte[0];
                }
            }
        }

        public Guid DocumentDetailId { get; set; }

        public HttpPostedFileBase Attachment { get; set; }

        public int DocumentTypeId { get; set; }

        public string DocumentBytesString { get; set; }

        public string DocumentFileName { get; set; }

        public string StudentCode { get; set; }
    }
}