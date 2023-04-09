using GonzStore.MailMessenger;
using GonzStore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GonzStore.Entities;
using GonzStore.Models;

namespace GonzStore.Controllers
{
    public class GioHangController : Controller
    {
        public IActionResult Index()
        {

            if (ssuser != null)
            {
                ViewBag.houser = ssuser.hoUser;
                ViewBag.tenuser = ssuser.tenUser;
                ViewBag.accmenu1 = "Thông tin cá nhân";
                ViewBag.accmenu2 = "Đơn mua";
                ViewBag.accmenu3 = "Thoát";
                if (ssuser.vaitro == "admin")
                {
                    ViewBag.accmenu4 = "Trang quản trị";
                }
                else if (ssuser.vaitro == "staff")
                {
                    ViewBag.accmenu4 = "Trang nhân viên";
                }
            }
            else
            {
                ViewBag.houser = "TÀI";
                ViewBag.tenuser = "KHOẢN";
                ViewBag.accmenu1 = "Đăng nhập";
            }
            return View(Carts);
        }

        public IActionResult AddTocart(int id,int sl)
        {
            
            if (sl == 0) { sl = 1; }
            var mycart = Carts;
            var item = mycart.SingleOrDefault(p => p.idSP == id);

            if (item == null)
            {
                var sp = _context.SanPhams.SingleOrDefault(p => p.idSP == id);
                item = new SSGioHang
                {

                    idSP = id,
                    tensp = sp.tenSP,
                    hinhAnh = sp.hinhAnh,
                    giasp = sp.giaSP,
                    soLuong = sl,
                };

                mycart.Add(item);
            }
            else
            {
                item.soLuong+=sl;
            }

            HttpContext.Session.Set("GioHang", mycart);
            return RedirectToAction("Index");
        }

        private readonly MyDBContext _context;

        public GioHangController(MyDBContext context)
        {
            _context = context;
        }

        public List<SSGioHang> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<SSGioHang>>("GioHang");
                if (data == null)
                {
                    data = new List<SSGioHang>();
                }
                return data;
            }
        }


        public sessionuser ssuser
        {
            get
            {
                var data = HttpContext.Session.Get<sessionuser>("ssuser");
                /* if (data == null)
                 {
                     data = new sessionuser();
                 }*/
                return data;
            }
        }

        [HttpPost]
        public IActionResult AddTocartAjax(int id, int sl)
        {

            if (sl == 0) { sl = 1; }
            var mycart = Carts;
            var item = mycart.SingleOrDefault(p => p.idSP == id);

            if (item == null)
            {
                var sp = _context.SanPhams.SingleOrDefault(p => p.idSP == id);
                item = new SSGioHang
                {

                    idSP = id,
                    tensp = sp.tenSP,
                    hinhAnh = sp.hinhAnh,
                    giasp = sp.giaSP,
                    soLuong = sl,
                };

                mycart.Add(item);
            }
            else
            {
                item.soLuong += sl;
            }

            HttpContext.Session.Set("GioHang", mycart);
            return View();
        }

      [HttpPost]
        public IActionResult checkmgg(string id)
        {
            var mgg = _context.MaGiamGias
                .FirstOrDefault(p => p.codeMGG == id);

            if (mgg == null || mgg.soLuong<=0 || mgg.trangThai=="Ẩn") {
                ViewBag.mess = "-1";
            }
            else
            {
                ViewBag.mess = mgg.giaTri;

            }


        
            return View();       
        }

        [HttpPost]
        public IActionResult tongtien()
        {

            ViewBag.mess = Carts.Sum(p => p.thanhTien);


            return View();
        }


        public IActionResult checkout(int mggvl,string mggvlcode, int tth, int tongcong)
        {

            if (Carts.Count==0)
            {
                RedirectToAction("index", "GioHang");
            }
        
            if (ssuser != null)
            {
                ViewBag.houser = ssuser.hoUser;
                ViewBag.tenuser = ssuser.tenUser;
                ViewBag.accmenu1 = "Thông tin cá nhân";
                ViewBag.accmenu2 = "Đơn mua";
                ViewBag.accmenu3 = "Thoát";
                if (ssuser.vaitro == "admin")
                {
                    ViewBag.accmenu4 = "Trang quản trị";
                }
                else if (ssuser.vaitro == "staff")
                {
                    ViewBag.accmenu4 = "Trang nhân viên";
                }
            }
            else
            {
                ViewBag.houser = "TÀI";
                ViewBag.tenuser = "KHOẢN";
                ViewBag.accmenu1 = "Đăng nhập";
            }
            ViewBag.mggvlcode = mggvlcode;
            ViewBag.mggvl = mggvl;
            ViewBag.tth = tth;
            ViewBag.tongcong = tongcong;
            if (ssuser != null)
            {
  ViewBag.ho = ssuser.hoUser;
            ViewBag.ten = ssuser.tenUser;
            ViewBag.email = ssuser.emailUser;
            ViewBag.sdt = ssuser.sdtUser;
            ViewBag.diachi = ssuser.diaChi;
            }
          


            return View();
        }



        public IActionResult xacnhancheckout(int mggvl,string mggvlcode, int tth, int tongcong, string ho, string ten, string diachi, string email,string sdt, string ghichu )
        {

            if (ssuser != null)
            {
                ViewBag.houser = ssuser.hoUser;
                ViewBag.tenuser = ssuser.tenUser;
                ViewBag.accmenu1 = "Thông tin cá nhân";
                ViewBag.accmenu2 = "Đơn mua";
                ViewBag.accmenu3 = "Thoát";
                if (ssuser.vaitro == "admin")
                {
                    ViewBag.accmenu4 = "Trang quản trị";
                }
                else if (ssuser.vaitro == "staff")
                {
                    ViewBag.accmenu4 = "Trang nhân viên";
                }
            }
            else
            {
                ViewBag.houser = "TÀI";
                ViewBag.tenuser = "KHOẢN";
                ViewBag.accmenu1 = "Đăng nhập";
            }

            if (sdt == null ||email==null)
            {
                return RedirectToAction("index", "Home");
            }
            var mggcode = _context.MaGiamGias.FirstOrDefault(p => p.codeMGG == mggvlcode);

            if (mggcode != null)
            {
                mggcode.soLuong--;
                _context.Update(mggcode);
                _context.SaveChanges();
            }



            var db  = _context.DonHangs
                //      .Where(p => p.luotMua > 100)              // Lọc các sản phẩm giá trên 100
                .OrderByDescending(p => p.idDH)        // Sắp xếp giảm dần, tăng dần là OrderBy
                .Take(1); ;


            
            DateTime xx = DateTime.Now;

            DonHang dh = new DonHang();
          if (ssuser != null)
            {
                dh.idUser = ssuser.idUser;
            }
            dh.trangThai = "Đang xử lý";
            dh.ngayCapNhat = xx;
            dh.ngayDat = xx;
            dh.hoDH = ho;
            dh.tenDH = ten;
            dh.sdtDH = sdt;
            dh.diaChi = diachi;
            dh.eMail = email;
            dh.ghiChu = ghichu;
            dh.tongTienHang = tth;
            dh.tongSoTien = tongcong;
            dh.maGiamGiaDH = mggvl;

            int x = insert(dh);

            

           

            foreach (var item in Carts)
            {
                ChiTietDonHang xxx = new ChiTietDonHang();

                xxx.idDH = x;
                xxx.tenSP = item.tensp;
                xxx.hinhSP = item.hinhAnh;
                xxx.donGia = item.giasp;
                xxx.soLuong = item.soLuong;
                xxx.thanhTien = item.thanhTien;
                

                _context.Add(xxx);
                _context.SaveChanges();

                var ct = _context.SanPhams
                    .FirstOrDefault(p => p.idSP == item.idSP);
                ct.luotMua++;
                ct.soLuongKho--;

                _context.Update(ct);
                _context.SaveChanges();
            }




            var ss = new List<SSGioHang>();

                HttpContext.Session.Set("GioHang", ss);
            if (ssuser == null)
            {
ViewBag.mess = "Đặt hàng thành công! Bạn có thể dùng mã này để tra cứu trạng thái đơn hàng: " + x;
            }
            else
            {
                ViewBag.mess = "Đặt hàng thành công!";
            }

            ViewBag.iddh = x;
            ViewBag.emails = email;

            /*
                        ViewBag.mess=x;
                        return View();*/
            return View();
        }

     


            public int insert(DonHang dh)
        {
            _context.Add(dh);
            _context.SaveChanges();
            return dh.idDH;
        }


        public IActionResult hoanthanh(int xx)
        {



            //var db = _context.DonHangs.FirstOrDefault(p => p.ngayDat == xx);

            /*  foreach (var item in Carts)
              {
                  ChiTietDonHang xxx = new ChiTietDonHang();

                  xxx.idDH = xx;
                  xxx.tenSP = item.tensp;
                  xxx.hinhSP = item.hinhAnh;
                  xxx.donGia = item.giasp;
                  xxx.soLuong = item.soLuong;
                  xxx.thanhTien = item.thanhTien;
                  _context.Add(xxx);
                  await _context.SaveChangesAsync();

              }


              Carts.Clear();

              HttpContext.Session.Set("GioHang", Carts);*/
            ViewBag.mess = xx;

            return View();
          //  return RedirectToAction("index", "giohang");
        }




        public IActionResult xoasp(int? id)
        {
            var data = Carts;

            var item = data.SingleOrDefault(p => p.idSP == id);
            data.Remove(item);
            HttpContext.Session.Set("GioHang", data);

            return RedirectToAction("index", "GioHang");
        }




    }
}
