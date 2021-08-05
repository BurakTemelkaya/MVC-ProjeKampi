using EntityLayer.Concrete;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();
        void AdminAdd(Admin admin);
        Admin GetByID(int id);
        AdminLogInDto GetByIDDto(int id);
        void AdminDelete(Admin admin);
        void AdminUpdate(Admin admin);
    }
}
