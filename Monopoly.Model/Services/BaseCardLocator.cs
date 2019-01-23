using Monopoly.Model.Abstract;
using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;
using static Monopoly.Model.Abstract.AbstractCard;
using static Monopoly.Model.Abstract.AbstractRealtyCard;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.Services
{
    public class BaseCardLocator: ICardLocator
    {
        public ObservableCollection<AbstractCard> GetCardSet()
        {
            var blueCardGroup = new TownCardGroup(Colors.LightBlue);
            var brownCardGroup = new TownCardGroup(Colors.Brown);
            var aquaCardGroup = new TownCardGroup(Colors.Aquamarine);
            var grayCardGroup = new TownCardGroup(Colors.Gray);
            var greenCardGroup = new TownCardGroup(Colors.Green);
            var yellowCardGroup = new TownCardGroup(Colors.Yellow);
            var pinkCardGroup = new TownCardGroup(Colors.Pink);
            var orangeCardGroup = new TownCardGroup(Colors.Orange);
            var redCardGroup = new TownCardGroup(Colors.Red);
            var stationsGroup = new StationCardGroup();


            return new ObservableCollection<AbstractCard>()
            {
                //start corner
                new EventCard("Chest", DefaultImagesLocator.GetEventPicture("start.png"), (gm) => { }),

                //chest
                new EventCard("Chest", DefaultImagesLocator.GetEventPicture("chest.png"), (gm) => gm.ChestAction()),

                //blue group
                new TownCard("Livia", 50, 25, 45, blueCardGroup, new TaxGroup(5, 10, 18, 30, 90, 160, 250), CardOrientation.BOTTOM),
                new TownCard("Sudan", 60, 30, 50, blueCardGroup, new TaxGroup(10, 20, 30, 60, 180, 320, 450), CardOrientation.BOTTOM),

                //some event card
                new EventCard("LuckyWheel", DefaultImagesLocator.GetEventPicture("luckwheel.png"), (gm) => {gm.GameAction( new GameAction("You won prize!", man => man.GiveMoney(200))); }),

                //station card
                new StationCard("Japan station", 200, 100, stationsGroup, new TaxGroup(50, 100, 150, 200), CardOrientation.BOTTOM),

                //some event card
                new EventCard("Lucky", DefaultImagesLocator.GetEventPicture("luck.png"), (gm) => gm.LuckyAction()),

                //brown group
                new TownCard("Turkey", 100, 50, 50, brownCardGroup, new TaxGroup(10, 20, 40, 90, 270, 400, 550), CardOrientation.BOTTOM),
                new TownCard("Greece", 100, 50, 50, brownCardGroup, new TaxGroup(10, 20, 40, 90, 270, 400, 550), CardOrientation.BOTTOM),
                new TownCard("Bolgaria", 120, 60, 50, brownCardGroup, new TaxGroup(16, 32, 50, 100, 300, 450, 600), CardOrientation.BOTTOM),

                //jail corner
                new EventCard("Jail", DefaultImagesLocator.GetEventPicture("jail.png"), null),

                //aquamarine group
                new TownCard("Poland", 150, 75, 100, aquaCardGroup, new TaxGroup(16, 32, 50, 150, 450, 625, 750), CardOrientation.LEFT),
                new TownCard("Russia", 150, 75, 100, aquaCardGroup, new TaxGroup(16, 32, 50, 150, 450, 625, 750), CardOrientation.LEFT),
                new TownCard("Ukraine", 180, 90, 100, aquaCardGroup, new TaxGroup(20, 40, 60, 180, 500, 700, 900), CardOrientation.LEFT),

                //gray group
                new TownCard("Vatican", 0, 100, 100, grayCardGroup, new TaxGroup(22, 44, 44, 88, 555, 666, 777), CardOrientation.LEFT),

                //station card
                new StationCard("Spain station", 200, 100, stationsGroup, new TaxGroup(60, 110, 160, 210), CardOrientation.LEFT),

                //chest
                new EventCard("Chest", DefaultImagesLocator.GetEventPicture("chest.png"), (gm) => gm.ChestAction()),

                //green group
                new TownCard("Litva", 200, 100, 100, greenCardGroup, new TaxGroup(20, 40, 70, 200, 550, 750, 950), CardOrientation.LEFT),
                new TownCard("Latvia", 200, 100, 100, greenCardGroup, new TaxGroup(20, 40, 70, 200, 550, 750, 950), CardOrientation.LEFT),
                new TownCard("Estonya", 220, 110, 100, greenCardGroup, new TaxGroup(30, 60, 80, 220, 600, 800, 1000), CardOrientation.LEFT),
                
                //parking corner
                new EventCard("Parking", DefaultImagesLocator.GetEventPicture("parking.png"), null),

                //yellow group
                new TownCard("Norvey", 220, 110, 150, yellowCardGroup, new TaxGroup(30, 60, 90, 250, 700, 875, 1050), CardOrientation.TOP),
                new TownCard("Swiss", 220, 110, 150, yellowCardGroup, new TaxGroup(30, 60, 90, 250, 700, 875, 1050), CardOrientation.TOP),
                new TownCard("Finland", 220, 110, 150, yellowCardGroup, new TaxGroup(40, 80, 100, 300, 750, 925, 1100), CardOrientation.TOP),
                
                //event
                new EventCard("Lucky", DefaultImagesLocator.GetEventPicture("luck.png"), (gm) => gm.LuckyAction()),

                //station card
                new StationCard("USA station", 200, 100, stationsGroup, new TaxGroup(70, 120, 170, 220), CardOrientation.TOP),

                //event
                new EventCard("LuckyWheel", DefaultImagesLocator.GetEventPicture("luckwheel.png"), (gm) => {gm.GameAction( new GameAction("You won prize!", man => man.GiveMoney(200))); }),

                //pink group
                new TownCard("Germany", 280, 140, 150, pinkCardGroup, new TaxGroup(40, 80, 110, 330, 800, 975, 1150), CardOrientation.TOP),
                new TownCard("France", 280, 140, 150, pinkCardGroup, new TaxGroup(40, 80, 110, 330, 800, 975, 1150), CardOrientation.TOP),
                new TownCard("Great Britain", 300, 150, 150, pinkCardGroup, new TaxGroup(50, 100, 120, 360, 850, 1025, 1200), CardOrientation.TOP),
                
                //go to jail corner
                new EventCard("Court", DefaultImagesLocator.GetEventPicture("court.png"), (gm) => gm.GameAction( new GameAction("You are going to jail!", (g) => g.GoToJail()))),

                //orange group
                new TownCard("Canada", 300, 150, 200, orangeCardGroup, new TaxGroup(50, 100, 130, 390, 900, 1100, 1275), CardOrientation.RIGHT),
                new TownCard("Mexico", 300, 150, 200, orangeCardGroup, new TaxGroup(50, 100, 130, 390, 900, 1100, 1275), CardOrientation.RIGHT),
                new TownCard("USA", 320, 160, 200, orangeCardGroup, new TaxGroup(60, 120, 150, 450, 1000, 1200, 1400), CardOrientation.RIGHT),
                
                //chest
                new EventCard("Chest", DefaultImagesLocator.GetEventPicture("chest.png"), (gm) => gm.ChestAction()),

                //station
                new StationCard("UK station", 200, 100, stationsGroup, new TaxGroup(100, 150, 200, 250), CardOrientation.RIGHT),

                //event
                new EventCard("Lucky", DefaultImagesLocator.GetEventPicture("luck.png"), (gm) => gm.LuckyAction()),

                //orange group
                new TownCard("Katar", 360, 180, 200, redCardGroup, new TaxGroup(60, 120, 175, 500, 1100, 1300, 1500), CardOrientation.RIGHT),
                new TownCard("China", 400, 200, 200, redCardGroup, new TaxGroup(80, 160, 200, 600, 1400, 1700, 2000), CardOrientation.RIGHT),

                //tax pay
                new EventCard("Tax", DefaultImagesLocator.GetEventPicture("tax.png"), (gm) => gm.GameAction( new GameAction("You pay tax", (man) => man.TakeMoney(200)))),
            };
        }
    }
}
