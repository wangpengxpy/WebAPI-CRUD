using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Flower : BaseEntity<int>
    {

        public string Remark { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
