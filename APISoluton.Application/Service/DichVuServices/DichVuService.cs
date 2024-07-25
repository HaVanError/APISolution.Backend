using APISolution.Database.DatabaseContext;
using APISolution.Database.Stored_Procedure;
using APISoluton.Application.Interface.IDichVu.Commands;
using APISoluton.Application.Interface.IDichVu.Queries;
using APISoluton.Database.ViewModel.DichVuView;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.DichVuServices
{
    public class DichVuService : IDichVuCommand, IDichVuQueries
    {
        private readonly DdConnect _db;
        private  readonly IMapper _mapper;
        private readonly ProcedureDichVu _procedureDichVu;
        public DichVuService(DdConnect db, IMapper mapper, ProcedureDichVu procedureDichVu)
        {
            _db = db;
            _mapper = mapper;
             _procedureDichVu = procedureDichVu;
        }

        public async Task<DichVuVM> AddDichVu(DichVuVM model)
        {
           return  await _procedureDichVu.CreatDichVuStored(model);
        }

        public async Task DeleteDichVu(int id)
        {
            var check = _db.DichVus.SingleOrDefault(x=>x.IdDichVu == id);
            if (check != null)
            {
                await _procedureDichVu.DeleteDichVu(id);
 
            }
        }

        public async Task<List<DichVuVM>> GetAllDichVu(int pageNumber  , int pageSize)
        {
            var list =  await _db.DichVus.ToListAsync();
            var resual = list.Select(x => new DichVuVM
            {
                NameDichVu = x.NameDichVu,
                SoLuong = x.SoLuong,
                Gia = x.Gia,
            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return resual;
        }

        public async Task<DichVuVM> GetByIdDichVu(int id)
        {
           
            var dichVu = await _procedureDichVu.GetByIdDichVu(id);
               var map = _mapper.Map<DichVuVM>(dichVu);
            return map;
        }

        public async Task<DichVuVM> UpdateDichVu(DichVuVM model, int id)
        {
            var check = await _db.DichVus.SingleOrDefaultAsync(x => x.IdDichVu == id);
            if (check != null)
            {
              await _procedureDichVu.UpdateDichVuStored(id, model);
                var map = _mapper.Map<DichVuVM>(check); 
                return map;
            }
            else
            {
                 throw new NotImplementedException(); 
            }
        }
    }
}
