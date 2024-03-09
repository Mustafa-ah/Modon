using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
   public class AttachmentModel
    {
        public string title { get; set; }
        public byte[] content { get; set; }
        public int fK_CreatedBy { get; set; }
        public DateTime creationDate { get; set; }
        public int FileSize { get; set; }
    }
}
