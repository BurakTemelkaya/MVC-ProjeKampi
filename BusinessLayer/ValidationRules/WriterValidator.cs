using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar Adını Boş Geçemezsiniz");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar Soyadını Boş Geçemezsiniz");
            // RuleFor(x => x.WriterAbout).Must(ContainsIsA).WithMessage("Hakkımda kısmında a harfi içeren bir kelime giriniz");// Ödev
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmını Boş Geçemezsiniz");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan kısmını Boş Geçemezsiniz");
            //RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Bırakılamaz");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail Adresi Boş Bırakılamaz");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen En Az 2 Karekter Girişi Yapın");
            RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Lütfen 50 Karekterden Fazla Değer Girişi Yapmayın");
            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("Lütfen En Az 2 Karekter Girişi Yapın");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("Lütfen 50 Karekterden Fazla Değer Girişi Yapmayın");
            RuleFor(x => x.WriterMail).EmailAddress().WithMessage("Lütfen Sadece Mail Adresi Giriniz");
        }
        public bool ContainsIsA(string name)
        {
            bool value = name.Contains("a");//yazarın hakkında kısmında mutlaka a kısmı olsun
            return value;
        }
    }
}
