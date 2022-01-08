using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class StokKartValidator:AbstractValidator<StokKart>
    {
        public StokKartValidator()
        {
           RuleFor(p => p.AnaBirim).NotEmpty().WithMessage("Bu Alanlar Boş Bırakılamaz");
           RuleFor(p => p.StokAd).NotEmpty().WithMessage("Bu Alanlar Boş Bırakılamaz");
           RuleFor(p => p.StokTur).NotEmpty().WithMessage("Bu Alanlar Boş Bırakılamaz");
        }
    }
}
