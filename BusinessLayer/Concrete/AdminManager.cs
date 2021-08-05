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
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AdminDelete(Admin admin)
        {
            _adminDal.Delete(admin);
        }

        public void AdminUpdate(Admin admin)
        {
            _adminDal.Update(admin);
        }

        public void AdminAdd(Admin admin)
        {
            _adminDal.Insert(admin);
        }
        public Admin GetByID(int id)
        {
            return _adminDal.Get(x => x.AdminID == id);
        }
        public AdminLogInDto GetByIDDto(int id)
        {
            var admin = _adminDal.Get(x => x.AdminID == id);
            //admini adminlogindto'ya çevirme
            var adminlogIndto = new AdminLogInDto
            {
                AdminID = admin.AdminID,
                AdminUserName = admin.AdminUserName,
                AdminRole = admin.AdminRole,
                AdminStatus=admin.AdminStatus
            };
            return adminlogIndto;
        }

        public List<Admin> GetList()
        {
            return _adminDal.List();
        }

        
    }
}
