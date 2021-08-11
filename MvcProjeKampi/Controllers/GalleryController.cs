using BusinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcProjeKampi.Controllers
{
    public class GalleryController : Controller
    {
        ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());
        public ActionResult Index()
        {
            var files = ifm.GetList();
            return View(files);
        }
        [HttpGet]
        public ActionResult AddImage()
        {
            ImageViewModel imageModel = new ImageViewModel() { FileAttach = null, Message = string.Empty, IsValid = false };

            try { }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.View(imageModel);
        }
        [HttpPost]
        public ActionResult AddImage(ImageViewModel imageModel, ImageFile imageFile)
        {
            //Eger dosya yolu mevcut degilse,yeni dosya yolu olustur
            string folderPath = Server.MapPath("~/AdminLTE-3.0.4/images/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            try
            {
                // Dogrulama Islemleri
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string expansion = Path.GetExtension(Request.Files[0].FileName);
                    string path = "/AdminLTE-3.0.4/images/" + fileName + expansion;

                    //Dosya yükleme istek kontrolü
                    if (Request.Files.Count > 0)
                    {
                        var fullPath = Server.MapPath("/AdminLTE-3.0.4/images/") + fileName + expansion;
                        //Resim dosyasi isim kontrolü
                        if (System.IO.File.Exists(fullPath))
                        {
                            ViewBag.ActionMessage = "Bu dosya adında başka bir resim mevcuttur";
                        }
                        else
                        {
                            Request.Files[0].SaveAs(Server.MapPath(path));
                            imageFile.ImageName = fileName;
                            imageFile.ImagePath = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                            imageFile.ImageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                            ifm.ImageAdd(imageFile);
                            //ViewBag.ActionMessage = "Resim yükleme başarıyla gerçekleşmiştir.";
                            imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                            return RedirectToAction("Index");
                        }
                    }

                    // Ayarlar - Dogrulama gecerliyse 
                    //imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarılı.   ";
                    imageModel.IsValid = true;
                }
                else
                {
                    // Ayarlar - Dogrulama gecersizse  
                    imageModel.Message = "'" + imageModel.FileAttach.FileName + "' dosya yükleme başarısız!   ";
                    imageModel.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View(imageModel);
        }
        [HttpGet]
        public ActionResult UpdateImage(int id)//şu anda çalışmıyor
        {
            ImageViewModel updateModel = new ImageViewModel();


            List<SelectListItem> _valueImage = (from x in ifm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.ImageName,
                                                    Value = x.ImageID.ToString()
                                                }).ToList();
            ViewBag.valueImage = _valueImage;
            ViewData["Image"] = ifm.GetByIdImageFile(id);
            ViewData["Models"] = updateModel;
            return View(_valueImage);
        }

        [HttpPost]
        public ActionResult UpdateImage(ImageFile imageFile, string[] DynamicTextBox)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.Values = serializer.Serialize(DynamicTextBox);

            string message = "";

            foreach (string textboxValue in DynamicTextBox)
            {
                message = textboxValue;
            }
            ViewBag.Message = message;
            imageFile.ImageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ifm.ImageUpdate(imageFile);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteImage(int id)
        {
            var imageValues = ifm.GetByIdImageFile(id);
            ifm.ImageDelete(imageValues);
            return RedirectToAction("Index");
        }
    }
}