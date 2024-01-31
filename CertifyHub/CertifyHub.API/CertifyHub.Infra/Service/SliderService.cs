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
    public class SliderService : ISliderService
    {


        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public  async void DeleteSlider(int id)
        {
            _sliderRepository.DeleteSlider(id);
        }

        public async  Task<List<Slider>> GetAllSliders()
        {
            return await _sliderRepository.GetAllSliders();
        }

        public async Task<Slider> GetSliderByID(int id)
        {
           return await _sliderRepository.GetSliderByID(id);
        }

        public void InsertSlider(Slider slider)
        {
            _sliderRepository.InsertSlider(slider);
        }

        public void UpdateSlider(Slider slider)
        {
            _sliderRepository.UpdateSlider(slider);
        }
    }
}
