﻿using StokTakip_Deneme_.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace StokTakip_Deneme_.Controllers
{        [AllowAnonymous]
    //Çok önemli bir güvenlik komutu
    public class KullanicilarController : Controller
    {


        Stock_Tracking_AppEntities2 db = new Stock_Tracking_AppEntities2();
        // GET: Kullanicilar


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Kullanicilar p)
        {
            var kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == p.KullaniciAdi && x.Sifre == p.Sifre);

            if (kullanici != null)
            {


                FormsAuthentication.SetAuthCookie(p.KullaniciAdi, false);
                return RedirectToAction("Index", "Urunler");

            }

            ViewBag.hata = "Kullanıcı Adınızı veya Şifrenizi Yanlış girdiniz ";
            return View();

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ResetPasword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            var user = db.Kullanicilar.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                // Yeni bir rastgele şifre oluşturun
                string yeniSifre = GenerateRandomPassword(8);

                // Kullanıcının şifresini güncelleyin
                user.Sifre = yeniSifre;
                db.SaveChanges();

                // Şifre sıfırlama e-postasını gönder
                if (SendPasswordResetEmail(user, yeniSifre))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.hata = "E-posta gönderirken bir hata oluştu.";
                    return View();
                }
            }

            ViewBag.hata = "Girdiğiniz e-posta adresi sistemde bulunamadı.";
            return View();
        }

        private string GenerateRandomPassword(int length)
        {
            // Rastgele şifre oluşturma mantığını burada uygulayabilirsiniz
            // Örnek: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        }

        private bool SendPasswordResetEmail(Kullanicilar user, string yeniSifre)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("testMurat@gmail.com", "Şifre Sıfırlama");
                mail.To.Add(user.Email);
                mail.IsBodyHtml = true;
                mail.Subject = "Şifre Değiştirme İsteği";
                mail.Body = "Merhaba " + user.AdiSoyadi + "<br/>" +
                            "Kullanıcı Adınız: " + user.KullaniciAdi + "<br/>" +
                            "Yeni Şifreniz: " + yeniSifre;

                NetworkCredential credentials = new NetworkCredential("testMurat@gmail.com", "Kadir123");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                // Hata mesajını günlüğe kaydedebilirsiniz
                Console.WriteLine("E-posta gönderme hatası: " + ex.Message);
                return false;
            }
        }



        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Kayit(Kullanicilar p) 
        {


            if(!ModelState.IsValid)
            {
                 return View();
            }
            p.Rol = "musteri";
            db.Entry(p).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();   
            return RedirectToAction("Login");
        
        }


    }

} 







/*public ActionResult ResetPasword (Kullanicilar p )
            
        {
            var model = db.Kullanicilar.Where(x => x.Email == p.Email).FirstOrDefault(); 

            if (model != null) 
            {
                Guid rastgele = Guid.NewGuid();           // Guid rastgele bir sayı oluşturma sınıfı 
                model.Sifre=rastgele.ToString().Substring(0,8);
                db.SaveChanges();

                 SmtpClient sunucu = new SmtpClient("smtp.google.com", 587);  //587 nin bir sebebi yok genelde o sayı kullanılıyormuş 
                 sunucu.EnableSsl = true;
                MailMessage mail    = new MailMessage();    
                mail.From=new MailAddress("testMurat@yandex.com ","Şifre Sıfırlama");
                mail.To.Add(model.Email);
                mail.IsBodyHtml= true;
                mail.Subject = "Şifre Değiştirme İsteği";
                mail.Body += "Merhaba" + model.AdiSoyadi + "<br/>" + "Kullanıcı Adınız: " + model.KullaniciAdi 
                    + "<br/> Tek Kullanımlık Şifreniz: " + model.Sifre;
                NetworkCredential  net = new NetworkCredential("testMurat@yandex.com ","Murat123");   
                sunucu.Credentials = net;
                sunucu.Send(mail);
                return RedirectToAction("Login");
            
            }

            ViewBag.hata = "Girdiğiniz mail adresi Sistemdeki ile eşleşmiyor "; 
            return View();
        
        
        
        }}*/