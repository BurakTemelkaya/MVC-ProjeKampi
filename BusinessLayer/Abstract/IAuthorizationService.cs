using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using EntityLayer.Dto;

namespace BusinessLayer.Abstract
{
    public interface IAuthorizationService
    {
        Admin AdminHash(AdminLogInDto adminLogInDto);
        void AdminAdd(AdminLogInDto adminLogInDto);
        void AdminUpdate(AdminLogInDto adminLogInDto);
        bool AdminLogin(AdminLogInDto adminLogInDto, out int ID);

        Writer WriterRegister(WriterLogInDto writerLogInDto);

        void WriterAdd(WriterLogInDto writerLogInDto);
        void WriterUpdate(WriterLogInDto writerLogInDto);
        bool WriterLogIn(WriterLogInDto writerLogInDto);
    }
}
