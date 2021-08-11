using BusinessLayer.Abstract;
using DataAccesLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ImageFileManager : IImageFileService
    {
        IImageFileDal _imageFileDal;

        public ImageFileManager(IImageFileDal imageFileDal)
        {
            _imageFileDal = imageFileDal;
        }

        public ImageFile GetByIdImageFile(int id)
        {
            return _imageFileDal.Get(x => x.ImageID == id);
        }

        public List<ImageFile> GetList()
        {
            return _imageFileDal.List();
        }

        public void ImageAdd(ImageFile imageFile)
        {
            _imageFileDal.Insert(imageFile);
        }

        public void ImageDelete(ImageFile imageFile)
        {
            _imageFileDal.Delete(imageFile);
        }

        public void ImageUpdate(ImageFile imageFile)
        {
            _imageFileDal.Update(imageFile);
        }
    }
}
