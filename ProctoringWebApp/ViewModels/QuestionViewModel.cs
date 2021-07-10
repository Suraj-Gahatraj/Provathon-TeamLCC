using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProctoringWebApp.ViewModels
{
    public class QuestionViewModel
    {
       
        public int Semester { get; set; }
        public string SubjectCode { get; set; }

        public List<QuestionDetails> QuestionList { get; set; }
    }


    public class QuestionDetails
    {
        public int Id { get; set; }
        public string QuestionDescription { get; set; }
    }




}
