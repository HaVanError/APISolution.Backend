using APISolution.Database.DatabaseContext;
using APISolution.Database.ViewModel.ThanhToanView;
using APISoluton.Database.ViewModel.DichVuView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Stored_Procedure
{
   public class ProcedureThanhToan
    {
        private readonly DdConnect _db;
        public ProcedureThanhToan(DdConnect db) { 
            _db = db;
        }
        public async Task<ThanhToanAddVM> CreatThanhToanStored(ThanhToanAddVM model)
        {
            //model.NgayTraPhong = DateTime.Now;
            //string NgayTra = model.NgayTraPhong.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddThanhToan @IdPhieuDatPhong = {model.idPhieuDatPhong},@NgayTraPhong={model.NgayTraPhong}");

            return model;
            
        }
        public async Task DuyetThanhToanStored(int id)
        {
      await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DuyetThanhToan @IdThanhToan = {id}");

        }
    }
}
