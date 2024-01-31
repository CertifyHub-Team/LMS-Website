using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Repository
{
    public  interface ISliderRepository
    {

        void InsertSlider(Slider slider);
        Task <List<Slider>> GetAllSliders();

        Task<Slider> GetSliderByID(int id);

        void UpdateSlider(Slider slider);
        void DeleteSlider(int id);

    }
}
