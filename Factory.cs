using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureHunt2
{
   
        public static class Factory
        {
            public static int BackerNumber;
            public static Sovereigns ChooseSovereign(string SovereignID)
            {
                switch (SovereignID)
                {
                    case "King Esurience":
                        BackerNumber = 0;
                        return new Esurience();
                    case "Queen Cupidity":
                        BackerNumber = 1;
                        return new Cupidity();
                    case "King Rapacity":
                        BackerNumber = 2;
                        return new Rapacity();
                }
                return new Esurience();
            }
            public static int ShipNumber;
            public static Shipping ChooseShip(string ShipName)
            {
                switch (ShipName)
                {
                    case "Sapphire":
                        ShipNumber = 0;
                        return new Sapphire();
                    case "Ruby":
                        ShipNumber = 1;
                        return new Ruby();
                    case "Emerald":
                        ShipNumber = 2;
                        return new Emerald();
                    case "Topaz":
                        ShipNumber = 3;
                        return new Topaz();
                }
                return new Sapphire();
            }
            public static int PirateNumber;
            public static Shipping ChoosePirate(string PirateName)
            {
                switch (PirateName)
                {
                    case "Capt. Bluebeard":
                        PirateNumber = 0;
                        return new Sapphire();

                    case "Capt. RedCoat":
                        PirateNumber = 1;
                        return new Ruby();

                    case "Cmdr.GreenThumb":
                        PirateNumber = 2;
                        return new Emerald();

                    case "CmdrYellowBelly":
                        PirateNumber = 3;
                        return new Topaz();
                }
                return new Sapphire();
            }

        }
    }
