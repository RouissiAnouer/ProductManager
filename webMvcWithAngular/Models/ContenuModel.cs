using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web03.Models
{
    public class ContenuModel
    {
        public int ContenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
    }
}