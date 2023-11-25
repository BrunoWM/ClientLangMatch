using ClientLangMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLangMatch.Services
{
    public interface IArticleDataService
    {
        Task AddArticleAsync(Article article, int userId, int langId);
        Task<Article> GetArticleAsync(int id);
        Task<List<Article>> GetAllArticlesAsync();
        Task DeleteArticleAsync(int id);

       
    }
}
