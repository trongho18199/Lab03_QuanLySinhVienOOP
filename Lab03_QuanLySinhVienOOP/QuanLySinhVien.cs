using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVien
{
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSach = new List<SinhVien>();

        public List<SinhVien> LayDanhSach()
        {
            return _danhSach;
        }

        public bool Them(SinhVien sv, out string thongBao)
        {
            if (_danhSach.Any(s => s.MaSinhVien.Equals(sv.MaSinhVien, StringComparison.OrdinalIgnoreCase)))
            {
                thongBao = $"Lỗi: Mã sinh viên '{sv.MaSinhVien}' đã tồn tại trong danh sách!";
                return false;
            }
            _danhSach.Add(sv);
            thongBao = "Thêm sinh viên thành công!";
            return true;
        }

        public SinhVien TimTheoMa(string maSV)
        {
            return _danhSach.FirstOrDefault(s => s.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return _danhSach.Where(s => s.HoTen.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public bool SuaDiem(string maSV, double diemMoi, out string thongBao)
        {
            var sv = TimTheoMa(maSV);
            if (sv == null)
            {
                thongBao = "Không tìm thấy sinh viên có mã này!";
                return false;
            }
            sv.DiemTrungBinh = diemMoi;
            thongBao = "Cập nhật điểm thành công!";
            return true;
        }

        public bool Xoa(string maSV, out string thongBao)
        {
            var sv = TimTheoMa(maSV);
            if (sv == null)
            {
                thongBao = "Không tìm thấy sinh viên cần xóa!";
                return false;
            }
            _danhSach.Remove(sv);
            thongBao = "Xóa sinh viên thành công!";
            return true;
        }

        public List<SinhVien> SapXepTheoDiem()
        {
            return _danhSach.OrderByDescending(s => s.DiemTrungBinh).ToList();
        }

        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSach.Where(s => s.DiemTrungBinh >= 5.0).ToList();
        }
    }
}