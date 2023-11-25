using ClientLangMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLangMatch.Services
{
    public interface IPostDataService
    {
        Task AddPostAsync(Post article, int userId);
        Task<Post> GetPostAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task DeletePostAsync(int id);
    }
}
