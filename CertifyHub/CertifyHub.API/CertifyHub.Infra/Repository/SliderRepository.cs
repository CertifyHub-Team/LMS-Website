using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Repository
{
    public class SliderRepository : ISliderRepository
    {

        private readonly IDbContext _dbContext;
        public SliderRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public async void DeleteSlider(int id)
        {
            var p = new DynamicParameters();
            p.Add("slider_id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("Slider_Package.DeleteSlider", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Slider>> GetAllSliders()
        {
            var result = await _dbContext.Connection.QueryAsync<Slider>("Slider_Package.GetAllSliders", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<Slider> GetSliderByID(int id)
        {
            var p = new DynamicParameters(); 
            p.Add("slider_id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Slider>("Slider_Package.GetSliderByID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault(); //return single course 
        }


        public async void InsertSlider(Slider slider)
        {
            var p = new DynamicParameters();
            //p.Add("slider_id", slider.Sliderid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("image_path", slider.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("Slider_Package.InsertSlider", p, commandType: CommandType.StoredProcedure);
        }

        public async void UpdateSlider(Slider slider)
        {
            var p = new DynamicParameters();
            p.Add("slider_id", slider.Sliderid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("image_path", slider.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.ExecuteAsync("Slider_Package.UpdateSlider", p, commandType: CommandType.StoredProcedure);
        }
    }
}
