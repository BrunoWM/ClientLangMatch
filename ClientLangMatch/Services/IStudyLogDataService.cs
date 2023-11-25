using ClientLangMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLangMatch.Services
{
    public interface IStudyLogDataService
    {
        Task AddStudyLogAsync(StudyLog studyLog, int userId, int langId);
        Task<StudyLog> GetStudyLogAsync(int id);
        Task<List<StudyLog>> GetAllStudyLogsAsync();
        Task DeleteStudyLogAsync(int id);

    }
}
