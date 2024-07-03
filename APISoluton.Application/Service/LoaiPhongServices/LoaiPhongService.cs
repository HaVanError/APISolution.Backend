using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Stored_Procedure;
using APISoluton.Application.Interface.ILoaiPhong.Commands;
using APISoluton.Application.Interface.ILoaiPhong.Queries;
using APISoluton.Database.ViewModel.LoaiPhongView;
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
        static string _keyLoai = "loai";
        private readonly CacheServices.CacheServices _cacheServices;
        private readonly ProcedureLoaiPhong _procedureLoaiPhong;
        public LoaiPhongService(DdConnect db,IMapper mapper, CacheServices.CacheServices cacheServices , ProcedureLoaiPhong procedureLoaiPhong)
        {
            _db = db;
            _mapper = mapper;
            _cacheServices = cacheServices;
            _procedureLoaiPhong = procedureLoaiPhong;
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
               await _procedureLoaiPhong.CreatLoaiPhongStored(model);
                _cacheServices.Remove(_keyLoai);
                return model;
            }
        }

        public async Task<List<LoaiPhongVM>> GetAllLoaiPhong()
        {
         
            var list =  await _db.Loais.ToListAsync();
            var resul = list.Select(x => new LoaiPhongVM { Name = x.Name, MoTa = x.MoTa });
            _cacheServices.SetList(_keyLoai, resul.ToList(),TimeSpan.FromMinutes(30));
            return  resul.ToList();
        }

        public async Task<LoaiPhongVM> GetLoaiPhongById(int id)
        {
           var check = await _db.Loais.Where(x=>x.IdLoaiPhong==id).SingleOrDefaultAsync();
            if (check != null)
            {
                var loai = await _procedureLoaiPhong.GetByIdLoaiPhong(id);
                var map = _mapper.Map<LoaiPhongVM>(loai);
                _cacheServices.Remove(_keyLoai);
                return map;
      
            }
            else { return null; }
        }

        public async Task RemovePhong(int id)
        {
            var check = await _db.Loais.Where(x=>x.IdLoaiPhong ==  id).SingleOrDefaultAsync();
            if(check != null)
            {
                await _procedureLoaiPhong.DeleteLoaiPhong(id);
                _cacheServices.Remove(_keyLoai);
            }
            
        }

        public async Task UpdateLoaiPhong(LoaiPhongVM model, int id)
        {
            var check = await _db.Loais.Where(x=>x.IdLoaiPhong == id).SingleOrDefaultAsync();
            if (check != null)
            {
                await _procedureLoaiPhong.UpdateLoaiPhongStored(id, model);
                _cacheServices.Remove(_keyLoai);
            }

        }
    }
}
