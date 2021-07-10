using Microsoft.AspNetCore.Mvc;
using ProctoringWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProctoringWebApp.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index(int id)
        {
            var questionList = 
            
                new QuestionViewModel()
                {
                    Semester=6,
                    SubjectCode="CSC-204",
                    QuestionList= new List<QuestionDetails>()
                    {
                        new QuestionDetails()
                        {
                            Id=1,
                            QuestionDescription="What is OOPS?"
                        },
                         new QuestionDetails()
                        {
                            Id=1,
                            QuestionDescription="Explain Polymorphism with suitable example?"
                        },
                          new QuestionDetails()
                        {
                            Id=1,
                            QuestionDescription="What is Encapsulation"
                        },
                           new QuestionDetails()
                        {
                            Id=1,
                            QuestionDescription="How OOPs solve the problem of code duplication?"
                        },
                            new QuestionDetails()
                        {
                            Id=1,
                            QuestionDescription="What do you mean by class and objects?"
                        },
                             new QuestionDetails()
                        {
                            Id=1,
                            QuestionDescription="explain about function overloading"
                        }
                    }

            };
            return View(questionList);
        }

        
    }
}
