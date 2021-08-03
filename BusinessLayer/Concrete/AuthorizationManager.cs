using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Utilities;
using EntityLayer.Dto;

namespace BusinessLayer.Concrete
{
    public class AuthorizationManager : IAuthorizationService
    {
        IAdminService _adminService;
        IWriterService _writerService;
        public AuthorizationManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;
        }

        public AuthorizationManager(IAdminService adminService)
        {
            _adminService = adminService;

        }
        public void AdminAdd(string adminUserName, string adminMail, string adminPassword, string adminRole, bool adminStatus)
        {
            byte[] adminMailHash, adminPasswordHash, adminPasswordSalt;
            HashingHelper.AdminCreatePasswordHash(adminMail, adminPassword, out adminMailHash, out adminPasswordHash, out adminPasswordSalt);
            var admin = new Admin
            {
                AdminUserName = adminUserName,
                AdminMail = adminMailHash,
                AdminPasswordHash = adminPasswordHash,
                AdminPasswordSalt = adminPasswordSalt,
                AdminRole = adminRole,
                AdminStatus = adminStatus
            };
            _adminService.AdminAdd(admin);
        }

        public bool AdminLogin(AdminLoginDto adminLogInDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(adminLogInDto.AdminMail));
                var admin = _adminService.GetList();
                foreach (var item in admin)
                {
                    if (HashingHelper.AdminVerifyPasswordHash(adminLogInDto.AdminMail, adminLogInDto.AdminPassword, item.AdminMail,
                        item.AdminPasswordHash, item.AdminPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool WriterLogIn(WriterLoginDto writerLogInDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                //var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(writerLogInDto.WriterMail));
                var writer = _writerService.GetList();
                foreach (var item in writer)
                {
                    if (HashingHelper.WriterVerifyPasswordHash(writerLogInDto.WriterPassword,
                        item.WriterPasswordHash, item.WriterPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerMail, string writerPassword, bool WriterStatus)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(writerPassword, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterName = writerName,
                WriterSurName = writerSurName,
                WriterTitle = writerTitle,
                WriterAbout = writerAbout,
                WriterImage = writerImage,
                WriterMail = writerMail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterStatus = WriterStatus
            };
            _writerService.WriterAdd(writer);
        }
    }
}
