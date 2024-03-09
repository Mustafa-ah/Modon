using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
   public class FileDataModel:BaseEntity
    {
        public string FileName { get; set; }
        public byte[] content { get; set; }
        public string FileTime { get; set; }
        public string FileSize { get; set; } // for ui
        public int FileLenth { get; set; }//for size limit
        public string image { get; set; }
        public string filepath { get; set;  }
        public Guid AttachmentId { get; set; }
    }
}
