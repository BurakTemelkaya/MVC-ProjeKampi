using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjeKampi.Models
{
    public class HeadingByContentCount
    {
        public IEnumerable<Heading> Headings { get; set; }
        public IEnumerable<Content> Contents { get; set; }
    }
}