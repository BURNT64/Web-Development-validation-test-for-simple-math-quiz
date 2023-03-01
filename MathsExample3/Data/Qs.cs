using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MathsExample3.Data
{
    public class Qs
    {
        [Key]
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public decimal Answer { get; set; }
    }

    /*
     CREATE TABLE Questions
        QuestionID int IDENTITY(1,1) PRIMARY KEY,
        Question varChar(MAX),
        Answer decimal
     */
}
