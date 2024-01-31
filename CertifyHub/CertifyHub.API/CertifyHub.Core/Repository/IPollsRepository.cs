using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;

namespace CertifyHub.Core.Repository
{
    public interface IPollsRepository
    {

        void  CreatePoll(Poll poll);
        void UpdatePoll(Poll poll);
        void DeletePoll(int pollId);
        Task<Poll> GetPollById(int pollId);
        Task<List<Poll>> GetAllPolls();


    }
}
