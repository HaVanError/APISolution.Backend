using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Stored_Procedure;
using APISolution.Database.ViewModel.PhongView;
using APISoluton.Application.Interface.IPhong.Commands;
using APISoluton.Application.Interface.IPhong.Queries;
using APISoluton.Database.ViewModel.PhongView;
using APISoluton.Database.ViewModel.PhongView.PhongViewShowClient;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.PhongServices
{
    public class PhongService : IPhongCommand , IPhongQueries
    {
        private readonly DdConnect _db;
        private readonly IMapper _mapper;
        private readonly ProcedurePhong _procedurePhong;


        public PhongService(DdConnect db,IMapper mapper,ProcedurePhong procedurePhong)
        {
            _db = db;
            _mapper = mapper;
            _procedurePhong = procedurePhong;
        }
        public async Task<AddPhongView> AddPhong(AddPhongView model)
        { 
                var phong = await _procedurePhong.CreatPhongStored(model);
                return phong;
            
        }
        public  async Task<List<PhongVMShow>> GetAllPhong(int pageNumber,int pageSize)
        {
           
            var query = (from Phong in _db.Phongs
                         join LoaiPhong in _db.Loais on Phong.IdLoaiPhong equals LoaiPhong.IdLoaiPhong
                         where LoaiPhong.IdLoaiPhong == LoaiPhong.IdLoaiPhong
                         select new PhongVMShow
                         {
                             Name = Phong.Name,
                             Describe = Phong.Describe,
                             GiaPhong = Phong.GiaPhong,
                             LoaiPhong = LoaiPhong.Name,
                             StatusPhong = Phong.StatusPhong.ToString(),
                         }).AsNoTracking().Skip((pageNumber-1)*pageNumber).Take(pageSize).ToList();
            return   query;
        }

        public async Task<PhongVM> GetPhongById(int id)
        {
            var check = await _db.Phongs.Where(x=>x.IdPhong==id).FirstOrDefaultAsync();
            if(check != null)
            {
             var phong =     await _procedurePhong.GetByIdPhong(id);
             var map = _mapper.Map<PhongVM>(phong);
                return map;
            }
            return null;
        }

        public async Task RemovePhong(int id)
        {
            var check  = await _db.Phongs.Where(x=>x.IdPhong==id).SingleOrDefaultAsync();
            if (check != null)
            {
               await  _procedurePhong.DeletePhong(id);
            }
        }
        public async Task UpdatePhong(PhongVM model, int id)
        {
          var check = await _db.Phongs.Where(x=>x.IdPhong == id).SingleOrDefaultAsync();
            if (check != null)
            {
              await _procedurePhong.UpdatePhongStored(id, model);
            }

        }
    }
}
