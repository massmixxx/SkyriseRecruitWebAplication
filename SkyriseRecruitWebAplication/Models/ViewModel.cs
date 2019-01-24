using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyriseRecruitWebAplication.Models
{
    public class ViewModel
    {
        public RequestResult Result { get; set; }
        public List<KeyValuePair<String, int>> ListOfOccurence { get; set; }
        public int MaxValue { get; set; }
    }
}