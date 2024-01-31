using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
    public class AboutusService : IAboutusService
    {
        private readonly IAboutusRepository _aboutusRepository;

        public AboutusService(IAboutusRepository aboutusRepository)
        {
            _aboutusRepository = aboutusRepository;
        }
        public void CreateAboutUs(Aboutu Aboutus)
        {
            _aboutusRepository.CreateAboutUs(Aboutus); 
        }

        public Task<List<Aboutu>> GetAllAboutus()
        {
            return _aboutusRepository.GetAllAboutus();
        }

        public void UpdateAboutUs(Aboutu Aboutus)
        {
            _aboutusRepository.UpdateAboutUs(Aboutus);
        }
    }
}
