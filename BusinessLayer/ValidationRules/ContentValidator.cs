using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContentValidator : AbstractValidator<Content>
    {
        public ContentValidator()
        {
            RuleFor(x => x.ContentValue).NotEmpty().WithMessage("İçerik Boş Bırakılamaz");
            RuleFor(x => x.ContentValue).MaximumLength(999).WithMessage("En Fazla 1000 karekter girişi yapabilirsin");
        }
    }
}
