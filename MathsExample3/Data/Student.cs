using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MathsExample3.Data
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // do not auto-generate the key
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid student ID.")]
        public int StudentID { get; set; }
        public decimal Result { get; set; }
    }

    /*
         CREATE TABLE Students
            StudentID int PRIMARY KEY,
            Result decimal
    */
}
