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
    public class WriterLoginManager : IWriterLoginService
    {
        IWriterDal _writerDal;

        public WriterLoginManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        /*public Writer GetWriter(string mail, string password)
        {
            return _writerDal.Get(x=> x.WriterMail==mail && x.WriterPassword==password);
        }*/
    }
}
