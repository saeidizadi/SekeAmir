using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Main
{
    public class Setting
    {

        public int Id { get; set; }
        [DisplayName("نام شرکت")]
        public string CompanyName { get; set; }
        [DisplayName("قالب")]
        public string CurrentTheme { get; set; }
        [DisplayName("تجربه")]
        public int YearsOfExperience { get; set; }
        [DisplayName("کاربران")]
        public int HappyCustomers { get; set; }
        [DisplayName("تراکنش موفق")]
        public int SuccessfulTransactions { get; set; }
        [DisplayName("آدرس")]
        public string Address { get; set; }
        [DisplayName("شماره تماس")]
        public string Phone1 { get; set; }
        [DisplayName("شماره تماس")]
        public string Phone2 { get; set; }
        [DisplayName("ساعت کار")]
        public string WorkingHours { get; set; }
      

    }
}
