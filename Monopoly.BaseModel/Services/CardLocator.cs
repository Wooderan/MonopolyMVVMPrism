using Monopoly.BaseModel.Models;
using Monopoly.BaseModel.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monopoly.BaseModel.Services
{
    public static class CardLocator
    {
        public static ObservableCollection<AbstractCard> GetStandartCardSet()
        {
            var blueCardGroup = new CardGroup(Colors.Blue);
            var brownCardGroup = new CardGroup(Colors.Brown);
            var aquaCardGroup = new CardGroup(Colors.Aquamarine);
            var grayCardGroup = new CardGroup(Colors.Gray);
            var greenCardGroup = new CardGroup(Colors.Green);
            var yellowCardGroup = new CardGroup(Colors.Yellow);
            var pinkCardGroup = new CardGroup(Colors.Pink);
            var orangeCardGroup = new CardGroup(Colors.Orange);
            var redCardGroup = new CardGroup(Colors.Red);


            return new ObservableCollection<AbstractCard>()
            {
                //start corner
                null,

                //chest
                null,

                //blue group
                new TownCard("Livia", 50, 25, 50, blueCardGroup, new TaxGroup(5, 10, 18, 30, 90, 160, 250), CardOrientation.BOTTOM),
                new TownCard("Sudan", 60, 30, 50, blueCardGroup, new TaxGroup(10, 20, 30, 60, 180, 320, 450), CardOrientation.BOTTOM),

                //some event card
                null,

                //station card
                null,

                //some event card
                null,

                //brown group
                new TownCard("Turkey", 100, 50, 50, brownCardGroup, new TaxGroup(10, 20, 40, 90, 270, 400, 550), CardOrientation.BOTTOM),
                new TownCard("Greece", 100, 50, 50, brownCardGroup, new TaxGroup(10, 20, 40, 90, 270, 400, 550), CardOrientation.BOTTOM),
                new TownCard("Bolgaria", 120, 60, 50, brownCardGroup, new TaxGroup(16, 32, 50, 100, 300, 450, 600), CardOrientation.BOTTOM),

                //jail corner
                null,

                //aquamarine group
                new TownCard("Poland", 150, 75, 100, aquaCardGroup, new TaxGroup(16, 32, 50, 150, 450, 625, 750), CardOrientation.LEFT),
                new TownCard("Russia", 150, 75, 100, aquaCardGroup, new TaxGroup(16, 32, 50, 150, 450, 625, 750), CardOrientation.LEFT),
                new TownCard("Ukraine", 180, 90, 100, aquaCardGroup, new TaxGroup(20, 40, 60, 180, 500, 700, 900), CardOrientation.LEFT),

                //gray group
                new TownCard("Vatican", 0, 100, 100, grayCardGroup, new TaxGroup(22, 44, 44, 88, 555, 666, 777), CardOrientation.LEFT),

                //station card
                null,

                //chest
                null,

                //green group
                new TownCard("Litva", 200, 100, 100, greenCardGroup, new TaxGroup(20, 40, 70, 200, 550, 750, 950), CardOrientation.LEFT),
                new TownCard("Latvia", 200, 100, 100, greenCardGroup, new TaxGroup(20, 40, 70, 200, 550, 750, 950), CardOrientation.LEFT),
                new TownCard("Estonya", 220, 110, 100, greenCardGroup, new TaxGroup(30, 60, 80, 220, 600, 800, 1000), CardOrientation.LEFT),
                
                //parking corner
                null,

                //yellow group
                new TownCard("Norvey", 220, 110, 150, yellowCardGroup, new TaxGroup(30, 60, 90, 250, 700, 875, 1050), CardOrientation.TOP),
                new TownCard("Swiss", 220, 110, 150, yellowCardGroup, new TaxGroup(30, 60, 90, 250, 700, 875, 1050), CardOrientation.TOP),
                new TownCard("Finland", 220, 110, 150, yellowCardGroup, new TaxGroup(40, 80, 100, 300, 750, 925, 1100), CardOrientation.TOP),
                
                //event
                null,

                //station card
                null,

                //event
                null,

                //pink group
                new TownCard("Germany", 280, 140, 150, pinkCardGroup, new TaxGroup(40, 80, 110, 330, 800, 975, 1150), CardOrientation.TOP),
                new TownCard("France", 280, 140, 150, pinkCardGroup, new TaxGroup(40, 80, 110, 330, 800, 975, 1150), CardOrientation.TOP),
                new TownCard("Great Britain", 300, 150, 150, pinkCardGroup, new TaxGroup(50, 100, 120, 360, 850, 1025, 1200), CardOrientation.TOP),
                
                //go to jail corner
                null,

                //orange group
                new TownCard("Canada", 300, 150, 200, orangeCardGroup, new TaxGroup(50, 100, 130, 390, 900, 1100, 1275), CardOrientation.RIGHT),
                new TownCard("Mexico", 300, 150, 200, orangeCardGroup, new TaxGroup(50, 100, 130, 390, 900, 1100, 1275), CardOrientation.RIGHT),
                new TownCard("USA", 320, 160, 200, orangeCardGroup, new TaxGroup(60, 120, 150, 450, 1000, 1200, 1400), CardOrientation.RIGHT),
                
                //chest
                null,

                //station
                null,

                //event
                null,

                //orange group
                new TownCard("Katar", 360, 180, 200, redCardGroup, new TaxGroup(60, 120, 175, 500, 1100, 1300, 1500), CardOrientation.RIGHT),
                new TownCard("China", 400, 200, 200, redCardGroup, new TaxGroup(80, 160, 200, 600, 1400, 1700, 2000), CardOrientation.RIGHT),

                //tax pay
                null,
            };
        }
    }
}
