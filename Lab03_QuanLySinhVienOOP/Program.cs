using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_QuanLySinhVien
{
    internal class Program
    {
        private static QuanLySinhVien qlsv = new QuanLySinhVien();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            qlsv.Them(new SinhVien("SV001", "Nguyễn Văn A", new DateTime(2003, 5, 12), "CNTT01", 8.2), out _);
            qlsv.Them(new SinhVien("SV002", "Trần Thị B", new DateTime(2003, 10, 20), "CNTT01", 4.5), out _);

            int chon;
            do
            {
                Console.WriteLine("\n===== QUAN LY SINH VIEN =====");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Xuat danh sach");
                Console.WriteLine("3. Tim sinh vien theo ma");
                Console.WriteLine("4. Tim sinh vien theo ten");
                Console.WriteLine("5. Sua diem trung binh");
                Console.WriteLine("6. Xoa sinh vien");
                Console.WriteLine("7. Sap xep theo diem giam dan");
                Console.WriteLine("8. Loc sinh vien dat");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                {
                    Console.WriteLine("Vui lòng nhập một số hợp lệ!");
                    continue;
                }

                Console.WriteLine();
                switch (chon)
                {
                    case 1:
                        ChucNangThem();
                        break;
                    case 2:
                        InDanhSach(qlsv.LayDanhSach(), "DANH SÁCH TOÀN BỘ SINH VIÊN");
                        break;
                    case 3:
                        ChucNangTimTheoMa();
                        break;
                    case 4:
                        ChucNangTimTheoTen();
                        break;
                    case 5:
                        ChucNangSuaDiem();
                        break;
                    case 6:
                        ChucNangXoa();
                        break;
                    case 7:
                        InDanhSach(qlsv.SapXepTheoDiem(), "DANH SÁCH SINH VIÊN GIẢM DẦN THEO ĐIỂM");
                        break;
                    case 8:
                        InDanhSach(qlsv.LocSinhVienDat(), "DANH SÁCH SINH VIÊN ĐẠT (ĐIỂM >= 5)");
                        break;
                    case 0:
                        Console.WriteLine("Chương trình kết thúc. Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Chức năng không tồn tại. Vui lòng chọn lại!");
                        break;
                }
            } while (chon != 0);
        }

        private static void ChucNangThem()
        {
            Console.WriteLine("--- THÊM SINH VIÊN MỚI ---");
            Console.Write("Nhập mã sinh viên: ");
            string ma = Console.ReadLine()?.Trim();

            Console.Write("Nhập họ và tên: ");
            string ten = Console.ReadLine()?.Trim();

            DateTime ngaySinh;
            while (true)
            {
                Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine()?.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                    break;
                Console.WriteLine("Ngày sinh không đúng định dạng dd/MM/yyyy. Vui lòng nhập lại!");
            }

            Console.Write("Nhập mã lớp: ");
            string lop = Console.ReadLine()?.Trim();

            double diem;
            while (true)
            {
                Console.Write("Nhập điểm trung bình (0 - 10): ");
                if (double.TryParse(Console.ReadLine()?.Trim(), out diem))
                {
                    try
                    {
                        var sv = new SinhVien(ma, ten, ngaySinh, lop, diem);
                        if (qlsv.Them(sv, out string msg))
                        {
                            Console.WriteLine(msg);
                            return;
                        }
                        Console.WriteLine(msg);
                        return;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Điểm phải là kiểu số!");
                }
            }
        }

        private static void ChucNangTimTheoMa()
        {
            Console.Write("Nhập mã sinh viên cần tìm: ");
            string ma = Console.ReadLine()?.Trim();
            var sv = qlsv.TimTheoMa(ma);
            if (sv != null)
            {
                Console.WriteLine("\nTìm thấy sinh viên:");
                InTieuDeBang();
                Console.WriteLine(sv.LayThongTin());
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{ma}'!");
            }
        }

        private static void ChucNangTimTheoTen()
        {
            Console.Write("Nhập từ khóa họ tên cần tìm: ");
            string tuKhoa = Console.ReadLine()?.Trim();
            var ds = qlsv.TimTheoTen(tuKhoa);
            InDanhSach(ds, $"KẾT QUẢ TÌM KIẾM THEO TÊN: \"{tuKhoa}\"");
        }

        private static void ChucNangSuaDiem()
        {
            Console.Write("Nhập mã sinh viên cần sửa điểm: ");
            string ma = Console.ReadLine()?.Trim();

            double diemMoi;
            while (true)
            {
                Console.Write("Nhập điểm trung bình mới (0 - 10): ");
                if (double.TryParse(Console.ReadLine()?.Trim(), out diemMoi))
                {
                    try
                    {
                        if (qlsv.SuaDiem(ma, diemMoi, out string msg))
                        {
                            Console.WriteLine(msg);
                            return;
                        }
                        Console.WriteLine(msg);
                        return;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Điểm phải là dạng số!");
                }
            }
        }

        private static void ChucNangXoa()
        {
            Console.Write("Nhập mã sinh viên cần xóa: ");
            string ma = Console.ReadLine()?.Trim();
            if (qlsv.Xoa(ma, out string msg))
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        private static void InTieuDeBang()
        {
            Console.WriteLine(new string('-', 78));
            Console.WriteLine(string.Format("{0,-10} | {1,-20} | {2,-12} | {3,-10} | {4,-6} | {5,-10}",
                "Mã SV", "Họ Tên", "Ngày Sinh", "Mã Lớp", "Điểm", "Xếp Loại"));
            Console.WriteLine(new string('-', 78));
        }

        private static void InDanhSach(List<SinhVien> ds, string tieuDe)
        {
            Console.WriteLine($"\n--- {tieuDe} ---");
            if (ds == null || ds.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }
            InTieuDeBang();
            foreach (var sv in ds)
            {
                Console.WriteLine(sv.LayThongTin());
            }
            Console.WriteLine(new string('-', 78));
        }
    }
}