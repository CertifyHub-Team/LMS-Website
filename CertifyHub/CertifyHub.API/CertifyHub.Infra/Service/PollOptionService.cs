using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class PollOptionService : IPollOptionService
    {
        private readonly IPollOptionRepository _pollOptionRepository;

        public PollOptionService(IPollOptionRepository pollOptionRepository)
        {
            _pollOptionRepository = pollOptionRepository;
        }
        public async  void AddPollOption(Polloption pollOption)
        {
            _pollOptionRepository.AddPollOption(pollOption);
        }

        public async void DeletePollOption(int optionID)
        {
            _pollOptionRepository.DeletePollOption(optionID);
        }

        public async Task<List<Polloption>> GetPollOptions(int id)
        {
          return await   _pollOptionRepository.GetPollOptions(id);
        }
            public async void UpdatePollOption(Polloption pollOption)
        {
            _pollOptionRepository.UpdatePollOption(pollOption);
        }
    }
}
