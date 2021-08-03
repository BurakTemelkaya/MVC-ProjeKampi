using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ISkillService
    {
        List<Skill> GetList();
        List<Skill> GetList(string p);
        List<Skill> GetListBySkill(int id);
        void SkillAdd(Skill skill);
        Skill GetByID(int id);
        void SkillDelete(Skill skill);
        void SkillUpdate(Skill skill);
    }
}
