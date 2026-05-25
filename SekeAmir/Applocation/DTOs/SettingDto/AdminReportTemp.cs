using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SettingDto
{
    public class AdminReportTemp
    {
        public int CountRes { get; set; }
        public int CountCafe { get; set; }

        public float AveRes { get; set; }
        public float AveCafe { get; set; }
    }
}
