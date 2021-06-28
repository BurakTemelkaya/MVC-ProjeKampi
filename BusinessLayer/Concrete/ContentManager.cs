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
    public class ContentManager : IContentService
    {
        IContentDal _contentdal;

        public ContentManager(IContentDal contentdal)
        {
            _contentdal = contentdal;
        }

        public void ContentAdd(Content content)
        {
            throw new NotImplementedException();
        }

        public void ContentDelete(Content content)
        {
            throw new NotImplementedException();
        }

        public void ContentUpdate(Content content)
        {
            throw new NotImplementedException();
        }

        public Content GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Content> GetList()
        {
            return _contentdal.List();
        }

        public List<Content> GetListByHeadingID(int id)
        {
            return _contentdal.List(x => x.HeadingID == id);
        }
    }
}
