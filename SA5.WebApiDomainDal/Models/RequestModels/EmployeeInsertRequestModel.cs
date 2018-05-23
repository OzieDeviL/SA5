using SA5.WebApiDomainDal.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SA5.WebApiDomainDal.Models.RequestModels
{
    public class EmployeeInsertRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Range(1,100)]
        public int Office { get; set; }

        public static explicit operator Employee(EmployeeInsertRequestModel requestModel)
        {
            return new Employee()
            {
                Id = 0,
                Name = requestModel.Name,
                Office = requestModel.Office
            };
        }
    }
}
