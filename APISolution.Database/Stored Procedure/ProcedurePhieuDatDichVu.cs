using APISolution.Database.DatabaseContext;
using APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Stored_Procedure
{
    public class ProcedurePhieuDatDichVu
    {
        private readonly DdConnect _db;
        public ProcedurePhieuDatDichVu(DdConnect db)
        {
            _db = db;
        }
        public async Task UpdatePhieuDatDichVuProcedure(int id, AddPhieuDatDichVuView model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync(
             $"EXEC UpdatePhieuDatDichVu @idPhieuDatDichVu={id},@NameDichVu = {model.TenDichVu},  @idDichVu= {model.IdDichVu},@idPhieuDatPhong ={model.IdPhieuDatPhong}, @SoLuong={model.SoLuong}");
        }
        public async Task<AddPhieuDatDichVuView> AddPhieuDatDichVuProcedure(AddPhieuDatDichVuView model)
        {
            model.NgayDatDichVu = DateTime.Now;
            string ngayDatDichVuS= model.NgayDatDichVu.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            await _db.Database.ExecuteSqlInterpolatedAsync
                ($"EXECUTE AddPhieuDatDichVu @NameDichVu={model.TenDichVu},@idDichVu={model.IdDichVu},@idPhieuDatPhong={model.IdPhieuDatPhong},@SoLuong={model.SoLuong},@NgayDatDichVu={ngayDatDichVuS}");
            return model;
        }
        public async Task DeletePhieuDatDichVuProcedure(int Id)

        {
         
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DeletePhieuDatDichVu @id = {Id}");
           

        }
    }
}
