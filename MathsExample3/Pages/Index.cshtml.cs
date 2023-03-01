using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathsExample3.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace MathsExample3.Pages
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext _db;
        [BindProperty]
        public Student Stud { get; set; }
        public IList<Qs> ExamQuestions;
        [BindProperty]
        public IList<Answer> StudAnswers { get; set; }
        public decimal total = 0;
        public decimal percentage = -1;
        public string ErrorMessage { get; set; }
        public IndexModel(AppDbContext db)
        {
            _db = db;
            ExamQuestions = _db.Questions.FromSqlRaw("SELECT * FROM Questions").ToList();

        }

        public async Task<IActionResult> OnGetAsync()
        {
            await GetAvailableStudentIDs();
            return Page();
        }

        public async Task<IActionResult> OnPostCheckAsync()
        {
            // Check if student has already submitted
            var existingStudent = await _db.Students.FirstOrDefaultAsync(s => s.StudentID == Stud.StudentID);
            if (existingStudent != null)
            {
                ErrorMessage = "You have already submitted your answers.";
                await GetAvailableStudentIDs();
                return Page();
            }

            // Calculate grade
            foreach (Answer A in StudAnswers)
            {
                Qs Q = await _db.Questions.FindAsync(A.ID);
                if (Q.Answer == A.Ans)
                {
                    total += 1;
                }
            }
            percentage = (total / ExamQuestions.Count) * 100;
            Stud.Result = percentage;
            _db.Students.Add(Stud);
            await _db.SaveChangesAsync();

            await GetAvailableStudentIDs();
            return Page();
        }

        public IList<string> AvailableStudentIDs { get; set; }

        /*this allows students to check for unused student IDs however increasing the Enumerable.range reduced performance of the website*/
        private async System.Threading.Tasks.Task GetAvailableStudentIDs()
        {
            var allStudentIDs = await _db.Students.Select(s => s.StudentID).ToListAsync();
            AvailableStudentIDs = Enumerable.Range(1, 1000).Except(allStudentIDs).Select(i => i.ToString()).ToList();
        }
    }
}