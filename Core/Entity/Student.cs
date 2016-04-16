using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Student : BaseEntity<int>
    {

        public string Name { get; set; }


        public int FlowerId { get; set; }
        public virtual Flower Flower { get; set; }
    }
}
