using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutusController : ControllerBase
    {
        private readonly IAboutusService _aboutusService; 

        public AboutusController(IAboutusService aboutusService)
        {
            _aboutusService = aboutusService;
        }

        [HttpPost]
        public async void CreateAboutUs(Aboutu Aboutus)
        {
            _aboutusService.CreateAboutUs(Aboutus); 
        }

        [HttpPut]
        public async void UpdateAboutUs(Aboutu Aboutus)
        {
            _aboutusService.UpdateAboutUs(Aboutus);
        }

        [HttpGet]
        public async Task<List<Aboutu>> GetAllAboutus()
        {
            return await _aboutusService.GetAllAboutus();
        }

    }
}
