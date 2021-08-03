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
        void AdminAdd(string adminUserName, string adminMail, string adminPassword, string adminRole, bool adminStatus);
        bool AdminLogin(AdminLoginDto adminLogInDto);

        void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerUserName, string writerPassword, bool WriterStatus);
        bool WriterLogIn(WriterLoginDto writerLogInDto);
    }
}
