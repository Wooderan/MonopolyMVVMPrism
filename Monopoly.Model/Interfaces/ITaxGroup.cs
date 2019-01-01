using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface ITaxGroup
    {
        int Empty { get; set; }
        int Set { get; set; }
        int House { get; set; }
        int TwoHouses { get; set; }
        int ThreeHouses { get; set; }
        int FourHouses { get; set; }
        int Hotel { get; set; }
    }
}
