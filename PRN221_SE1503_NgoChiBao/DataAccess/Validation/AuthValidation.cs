using BusinessObject.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Validation
{
    //generic list - bỏ kiểu dữ liệu nào thì nó đều nhận -
    //khi runtime thì compiler sẽ tự hiểu dựa vào kiểu dữ liệu mình truyền vào
    // class the hien 1 thuc the ngoai doi
    public class AuthValidation : AbstractValidator<Member>
    {
        // List<T> - truyen vao kieu du lieu nao, no cung deu nhan het
        // List<int> a = new List<int>();
        // List<string> b = new List<string>();
        // List<float> c = new List<float>();

        // Constructor - trung ten voi ten class
        public AuthValidation()
        {
            //functional programming - chi quan tam toi ket qua sau khi moi dau chấm .
            //primitive programming - viet = tay - viet if else de thuc hien viec check 
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Can't be empty")
                                    .Length(20,50).WithMessage("Must be between 20 to 50!")
                                    .Matches("@fstore.com").WithMessage("Must be use email has @fstore.com"); //func programming
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Can't be empty")
                                    .Length(8,20).WithMessage("Must be between 8 to 20!");
        }

    }
}
