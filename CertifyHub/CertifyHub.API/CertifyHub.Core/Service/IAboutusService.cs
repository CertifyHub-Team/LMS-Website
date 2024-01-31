using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IAboutusService
    {
        void CreateAboutUs(Aboutu Aboutus);
        void UpdateAboutUs(Aboutu Aboutus);
        Task<List<Aboutu>> GetAllAboutus();

    }
}
