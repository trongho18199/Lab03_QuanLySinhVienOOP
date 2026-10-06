using System;
namespace Lab03_QuanLySinhVien
{
    public class SinhVien : Nguoi
    {
        private double _diemTrungBinh;

        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("Điểm trung bình phải nằm trong khoảng từ 0 đến 10!");
                }
                _diemTrungBinh = value;
            }
        }

        public SinhVien() { }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTB)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTB;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.0) return "Giỏi";
            if (DiemTrungBinh >= 6.5) return "Khá";
            if (DiemTrungBinh >= 5.0) return "Trung bình";
            return "Yếu";
        }

        public override string LayThongTin()
        {
            return string.Format("{0,-10} | {1,-20} | {2,-12:dd/MM/yyyy} | {3,-10} | {4,-6:F1} | {5,-10}",
                MaSinhVien, HoTen, NgaySinh, MaLop, DiemTrungBinh, XepLoai());
        }
    }
}