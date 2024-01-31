using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;

namespace CertifyHub.Infra.Service
{

   
    public class PollsService: IPollsService
    {
        private readonly IPollsRepository _pollsRepository;

        public PollsService(IPollsRepository pollsRepository)
        {
            _pollsRepository = pollsRepository;
        }

        public void CreatePoll(Poll poll)
        {
            _pollsRepository.CreatePoll(poll);
        }
        public void DeletePoll(int pollId)
        {
            _pollsRepository.DeletePoll(pollId);

        }

        public Task<List<Poll>> GetAllPolls()
        {
            return _pollsRepository.GetAllPolls();


        }

        public Task<Poll> GetPollById(int pollId)
        {
            return _pollsRepository.GetPollById(pollId);
        }

        public void UpdatePoll(Poll poll)
        {
            _pollsRepository.UpdatePoll(poll);

        }

    }
}
