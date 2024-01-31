using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }


        [HttpGet]
        [Route("GetSliders")]

        public async Task<List<Slider>> GetAllSliders()
        {
            return await _sliderService.GetAllSliders();
        }


        [HttpPost]
        public async void InsertSlider(Slider slider)
        {
            _sliderService.InsertSlider(slider);
        }

        [HttpPut]
        public async void UpdateSlider(Slider slider)
        {
            _sliderService.UpdateSlider(slider);
        }

        [HttpDelete("{id}")]  
        public async void DeleteSlider(int id)
        {
            _sliderService.DeleteSlider(id);
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<Slider> GetSliderByID(int id)
        {
            return await _sliderService.GetSliderByID(id);
        }

    }
}
