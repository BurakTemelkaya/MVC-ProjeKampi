using EntityLayer.Concrete;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWriterService
    {
        List<Writer> GetList();
        void WriterAdd(Writer writer);
        void WriteDelete(Writer delete);
        void WriterUpdate(Writer writer);
        Writer GetById(int id);
        WriterLogInDto GetByIdWriterDto(int id);
    }
}
