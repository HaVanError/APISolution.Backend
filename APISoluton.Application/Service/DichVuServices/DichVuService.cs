using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.DichVu.Commands;
using APISoluton.Application.Interface.DichVu.Queries;
using APISoluton.Application.ViewModel.DichVuView;
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
        private readonly IMapper _mapper;
        public DichVuService(DdConnect db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<DichVuVM> AddDichVu(DichVuVM model)
        {
            var map = _mapper.Map<DichVu>(model);
            await _db.DichVus.AddAsync(map);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task DeleteDichVu(int id)
        {
            var check = await _db.DichVus.FindAsync(id);
            if (check != null) { 
                _db.DichVus.Remove(check);
                await _db.SaveChangesAsync();

            }
        }

        public async Task<List<DichVuVM>> GetAllDichVu()
        {
            var list=  await _db.DichVus.AsNoTracking().ToListAsync();
            var result = list.Select(x => new DichVuVM
            {
                NameDichVu = x.NameDichVu,
                SoLuong = x.SoLuong,
                Gia = x.Gia,
            }).ToList();
            return result.ToList();



        }

        public async Task<IEnumerable<DichVuVM>> GetByIdDichVu(int id)
        {
            var check =  await _db.DichVus.FindAsync(id);
            if (check != null)
            {
                 var map = _mapper.Map<DichVuVM>(check);
                 return (IEnumerable<DichVuVM>)map;
            }
            else
            {
                return null ;
            }
        }

        public async Task<IEnumerable<DichVuVM>> UpdateDichVu(DichVuVM model, int id)
        {
           
            var check = await _db.DichVus.SingleOrDefaultAsync(x=>x.IdDichVu == id);
            if (check != null)
            {
                var map = _mapper.Map<DichVu>(model);
                _db.Entry(map).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return (IEnumerable<DichVuVM>)map;

            }
            else { return null; }
        }
    }
}
