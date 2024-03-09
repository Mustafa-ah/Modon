using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Maham.Bases;
using Xamarin.Forms;
using Maham.Service.Model.Response;

namespace Maham.Models
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool? IsEvidence { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
