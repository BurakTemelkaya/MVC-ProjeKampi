using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Utilities;
using EntityLayer.Dto;
using DataAccesLayer.EntityFramework;

namespace BusinessLayer.Concrete
{
    public class AuthorizationManager : IAuthorizationService
    {
        IAdminService _adminService;
        IWriterService _writerService;
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        private int _adminID;
        public AuthorizationManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;
        }

        public AuthorizationManager(IAdminService adminService)
        {
            _adminService = adminService;

        }
        public Admin AdminHash(AdminLogInDto adminLogInDto)
        {
            byte[] adminMailHash, adminMailSalt, adminPasswordHash, adminPasswordSalt;
            var admin = new Admin
            {
                AdminUserName = adminLogInDto.AdminUserName,
                AdminRole = adminLogInDto.AdminRole,
                AdminStatus = adminLogInDto.AdminStatus
            };
            _adminID = adminLogInDto.AdminID;
            var beforeAdminData = adminManager.GetByID(_adminID);
            if (adminLogInDto.AdminPassword != "" && adminLogInDto.AdminPassword != null)//password boş mu kontrolü
            {
                HashingHelper.AdminCreatePasswordHash(adminLogInDto.AdminPassword, out adminPasswordHash, out adminPasswordSalt);//password hash
                admin.AdminPasswordHash = adminPasswordHash;
                admin.AdminPasswordSalt = adminPasswordSalt;
            }
            else//veri boş olmasın diye eski veriyi veriyorum
            {
                admin.AdminPasswordHash = beforeAdminData.AdminPasswordHash;
                admin.AdminPasswordSalt = beforeAdminData.AdminPasswordSalt;
            }
            if (adminLogInDto.AdminMail != "" && adminLogInDto.AdminMail != null)
            {
                HashingHelper.AdminCreateMailHash(adminLogInDto.AdminMail, out adminMailHash, out adminMailSalt);//mail hash
                admin.AdminMailHash = adminMailHash;
                admin.AdminMailSalt = adminMailSalt;
            }

            else
            {
                admin.AdminMailHash = beforeAdminData.AdminMailHash;
                admin.AdminMailSalt = beforeAdminData.AdminMailSalt;
            }
            return admin;
        }
        public void AdminAdd(AdminLogInDto adminLogInDto)
        {
            var admin = AdminHash(adminLogInDto);
            _adminService.AdminAdd(admin);
        }
        public void AdminUpdate(AdminLogInDto adminLogInDto)
        {
            var admin = AdminHash(adminLogInDto);
            admin.AdminID = _adminID;
            _adminService.AdminUpdate(admin);
        }

        public bool AdminLogin(AdminLogInDto adminLogInDto, out int adminID)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(adminLogInDto.AdminMail));
                var admin = _adminService.GetList();
                foreach (var item in admin)
                {
                    if (HashingHelper.AdminVerifyPasswordHash(adminLogInDto.AdminPassword,
                        item.AdminPasswordHash, item.AdminPasswordSalt) && HashingHelper.AdminVerifyMailHash(adminLogInDto.AdminMail, item.AdminMailHash, item.AdminMailSalt))
                    {
                        adminID = item.AdminID;
                        return true;
                    }
                }
                adminID = 0;
                return false;
            }
        }

        public bool WriterLogIn(WriterLogInDto writerLogInDto)
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

        public Writer WriterRegister(WriterLogInDto writerLogInDto)
        {
            byte[] passwordHash, passwordSalt;

            var writer = new Writer
            {
                WriterName = writerLogInDto.WriterName,
                WriterSurName = writerLogInDto.WriterSurName,
                WriterTitle = writerLogInDto.WriterTitle,
                WriterAbout = writerLogInDto.WriterAbout,
                WriterImage = writerLogInDto.WriterImage,
                WriterMail = writerLogInDto.WriterMail,

                WriterStatus = writerLogInDto.WriterStatus
            };
            if (writerLogInDto.WriterPassword != null)
            {
                HashingHelper.WriterCreatePasswordHash(writerLogInDto.WriterPassword, out passwordHash, out passwordSalt);
                writer.WriterPasswordHash = passwordHash;
                writer.WriterPasswordSalt = passwordSalt;
            }
            return writer;
        }
        public void WriterAdd(WriterLogInDto writerLogInDto)
        {
            var writer = WriterRegister(writerLogInDto);
            writer.WriterID = writerLogInDto.WriterID;
            _writerService.WriterAdd(writer);
        }
        public void WriterUpdate(WriterLogInDto writerLogInDto)
        {
            var writer = WriterRegister(writerLogInDto);
            _writerService.WriterUpdate(writer);
        }
    }
}
