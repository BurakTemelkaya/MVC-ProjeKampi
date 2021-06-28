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
