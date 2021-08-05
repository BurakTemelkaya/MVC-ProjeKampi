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
        bool AdminLogin(AdminLogInDto adminLogInDto);

        void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerUserName, string writerPassword, bool WriterStatus);
        bool WriterLogIn(WriterLoginDto writerLogInDto);
    }
}
