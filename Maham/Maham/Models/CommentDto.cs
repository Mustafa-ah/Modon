using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Maham.Bases;
using Xamarin.Forms;
using Maham.Service.Model.Response;

namespace Maham.Models
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string creatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public string photoUrl { get; set; }
        public ImageSource backgroundimage { get; set; }
        public int CommentHeight { get; set; }

    }
}
