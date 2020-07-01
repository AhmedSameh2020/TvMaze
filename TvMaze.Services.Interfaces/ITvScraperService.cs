using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Model;

namespace TvMaze.Services.Interfaces
{
    public interface ITvScraperService
    {
        Task<List<Show>> GetShows();
        Task<List<Person>> GetShowCast(int showId);
        Task<List<Show>> GetShowsByPage(int page);
    }
}
