using APISolution.Database.DatabaseContext;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Stored_Procedure
{
    public class ProcedurePhieuDatPhong
    {
        private readonly DdConnect _db;
        public ProcedurePhieuDatPhong(DdConnect db) { 
        _db = db;

        }
        public async Task<AddPhieuDatPhongView>AddPhieuDatPhongProcedure( AddPhieuDatPhongView model)
        {
           //model.NgayDatPhong= DateTime.Now;
           // string ngayDatPhongString = model.NgayDatPhong.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddPhieuDatPhong @TenNguoiDat = {model.TenNguoiDat},@SDT = {model.SoDienThoai},@IdPhong={model.IdPhong},@NgayDatPhong={model.NgayDatPhong}");
            return model;
        }
    }
}
