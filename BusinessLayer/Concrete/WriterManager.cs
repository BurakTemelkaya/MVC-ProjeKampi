using BusinessLayer.Abstract;
using DataAccesLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {

        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetById(int id)
        {
            return _writerDal.Get(x => x.WriterID == id);
        }
        public Writer GetByMail(string WriterMail)
        {
            return _writerDal.Get(x => x.WriterMail == WriterMail);
        }
        public WriterLogInDto GetByIdWriterDto(int id)
        {
            var writer = _writerDal.Get(x => x.WriterID == id);
            //writerı writerlogindto'ya çevirme
            var writerLogInDto = new WriterLogInDto
            {
                WriterID = writer.WriterID,
                WriterName= writer.WriterName,
                WriterSurName = writer.WriterSurName,
                WriterImage = writer.WriterImage,
                WriterMail = writer.WriterMail,
                WriterAbout = writer.WriterAbout,
                WriterTitle = writer.WriterTitle            
            };
            return writerLogInDto;
        }

        public List<Writer> GetList()
        {
            return _writerDal.List();
        }

        public void WriteDelete(Writer delete)
        {
            _writerDal.Delete(delete);
        }

        public void WriterAdd(Writer writer)
        {
            _writerDal.Insert(writer);
        }

        public void WriterUpdate(Writer writer)
        {
            _writerDal.Update(writer);
        }
    }
}
