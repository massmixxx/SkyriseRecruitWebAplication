using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyriseRecruitWebAplication.Models
{
    public class RequestResult
    {

        public string Title { get; set; }
        public string Content { get; set; }
        public string Date_published { get; set; }
        public string Lead_image_url { get; set; }
        public string Dek { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string Excerpt { get; set; }
        public string Word_count { get; set; }
        public string Direction { get; set; }
        public string Total_pages { get; set; }
        public string Rendered_pages { get; set; }
        public string Next_page_url { get; set; }
        public string Author { get; set; }
        public string Messages { get; set; }
        public bool Error { get; set; }
    }
}