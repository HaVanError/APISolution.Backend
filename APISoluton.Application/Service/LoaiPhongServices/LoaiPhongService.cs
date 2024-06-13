using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.LoaiPhong.Commands;
using APISoluton.Application.Interface.LoaiPhong.Queries;
using APISoluton.Application.ViewModel.LoaiPhongView;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.LoaiPhongServices
{
    public class LoaiPhongService : ILoaiPhongCommand, ILoaiPhongQueries
    {
        private readonly DdConnect _db;
        private readonly IMapper _mapper; 
        public LoaiPhongService(DdConnect db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<LoaiPhongVM> AddLoaiPhong(LoaiPhongVM model)
        {
            var map = _mapper.Map <LoaiPhong>(model);
            var check = _db.Loais.SingleOrDefault(x=>x.IdLoaiPhong == map.IdLoaiPhong);
            if(check != null)
            {
                return null;
            }
            else
            {
                _db.Loais.Add(map);
                await _db.SaveChangesAsync();
                return model;
            }
        }

        public async Task<List<LoaiPhongVM>> GetAllLoaiPhong()
        {
            var list =  await _db.Loais.ToListAsync();
            var resul = list.Select(x => new LoaiPhongVM { Name = x.Name, MoTa = x.MoTa });
            return  resul.ToList();
        }

        public async Task<LoaiPhongVM> GetLoaiPhongById(int id)
        {
           var check = await _db.Loais.Where(x=>x.IdLoaiPhong==id).SingleOrDefaultAsync();
            if (check != null)
            {
                return new LoaiPhongVM
                {
                    Name = check.Name,
                    MoTa = check.MoTa
                };
            }
            else { return null; }
        }

        public async Task<string> RemovePhong(int id)
        {
            var check = await _db.Loais.Where(x=>x.IdLoaiPhong ==  id).FirstOrDefaultAsync();
            if(check != null)
            {
                _db.Loais.Remove(check);
                 await _db.SaveChangesAsync();
                return "Xóa Thành Công";
            }
            else
            {
                return "Error";
            }
        }

        public async Task<string> UpdatePhong(LoaiPhongVM model, int id)
        {
            var check = await _db.Loais.Where(x=>x.IdLoaiPhong == id).SingleOrDefaultAsync();
            if (check != null)
            {
                var map = _mapper.Map<LoaiPhong>(model);
                _db.Entry(map).State = EntityState.Modified;
               await _db.SaveChangesAsync();
                return "Cập nhật thành công ";

            }
            else
            {
                return "Error";
            }
        }
    }
}
