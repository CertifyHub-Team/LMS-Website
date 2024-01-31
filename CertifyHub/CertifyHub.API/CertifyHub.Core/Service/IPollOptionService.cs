using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IPollOptionService
    {
        void AddPollOption(Polloption pollOption);

        void UpdatePollOption(Polloption pollOption);

        void DeletePollOption(int optionID);


        Task<List<Polloption>> GetPollOptions(int id);

    }
}
