using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPData
{
    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, ShortName: {ShortName}, LongName: {LongName}";
        }
    }
}
