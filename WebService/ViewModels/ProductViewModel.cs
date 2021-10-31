using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.ViewModels
{
    public class ProductViewModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
