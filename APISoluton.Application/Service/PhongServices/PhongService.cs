using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.Phong.Commands;
using APISoluton.Application.Interface.Phong.Queries;
using APISoluton.Application.ViewModel.PhongView;
using APISoluton.Application.ViewModel.PhongView.PhongViewShowClient;
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
        public PhongService(DdConnect db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<PhongVM> AddPhong(PhongVM model)
        {
            var map = _mapper.Map<Phong>(model);
            var check =  await _db.Phongs.SingleOrDefaultAsync(x=>x.IdPhong == map.IdPhong);
            if (check !=null)
            {
                return null;
            }
            else
            {
                _db.Phongs.Add(map);
                await _db.SaveChangesAsync();
                return model;
            }  
        }
        public  async Task<List<PhongVMShow>> GetAllPhong()
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
                             
                         });
            return await query.ToListAsync();
        }

        public async Task<PhongVMShow> GetPhongById(int id)
        {
            var check = await _db.Phongs.Where(x=>x.IdPhong==id).FirstOrDefaultAsync();
            if(check != null)
            {
                var mapPhongVm = _mapper.Map<PhongVMShow>(check);
                return  new PhongVMShow { 
                    Name = mapPhongVm.Name,
                    LoaiPhong=mapPhongVm.LoaiPhong,
                    Describe = mapPhongVm.Describe,
                    GiaPhong= mapPhongVm.GiaPhong,
                    StatusPhong=check.StatusPhong.ToString(),
                };
            }
            return null;
        }

        public async Task<string> RemovePhong(int id)
        {
            var check  = await _db.Phongs.Where(x=>x.IdPhong==id).SingleOrDefaultAsync();
            if (check != null)
            {
                _db.Phongs.Remove(check);
                await _db.SaveChangesAsync();
                return "Xóa thành công";

            }
            else
            {
                return "Error";
            }
          
        }

        public async Task<string> UpdatePhong(PhongVM model, int id)
        {
          var check = await _db.Phongs.Where(x=>x.IdPhong == id).SingleOrDefaultAsync();
            if (check != null)
            {
                _db.Entry(check).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return "Thành Công";
            }
            else
            {
                return "Error";
            }

        }
    }
}
