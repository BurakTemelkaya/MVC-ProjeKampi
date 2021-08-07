using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dto
{
    public class HeadingCountContent
    {
        public IEnumerable<Heading> headings { get; set; }
        public IEnumerable <Content> contents { get; set; }
    }
}
