using Monopoly.Model.Interfaces;

namespace Monopoly.Model.Models
{
    public class TaxGroup : ITaxGroup
    {
        public int Empty { get; set; }
        public int Set { get; set; }
        public int House { get; set; }
        public int TwoHouses { get; set; }
        public int ThreeHouses { get; set; }
        public int FourHouses { get; set; }
        public int Hotel { get; set; }

        public TaxGroup(int empty, int set, int house, int twoHouses, int threeHouses, int fourHouses, int hotel)
        {
            Empty = empty;
            House = house;
            TwoHouses = twoHouses;
            ThreeHouses = threeHouses;
            FourHouses = fourHouses;
            Hotel = hotel;
        }
    }
}
