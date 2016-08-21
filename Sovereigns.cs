using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreasureHunt2
{
    //All the properties needed for the different sovereigns
  
        public abstract class Sovereigns
        {
            public string Name { get; set; }
            public int Gold { get; set; }
            public int AmountPromised { get; set; }
            public int ChoosePirate { get; set; }
            public string Cleared { get; set; }
            public PictureBox TreasureChest = null;
            public TextBox MyTextBox = new TextBox();
            public RadioButton rbPirate = null;
           
        }
    //  Assign chatacteristcs to each 
        class Esurience : Sovereigns
        {
            public Esurience()
            {
                Name = "King Esurience";
                Gold = 50;
                AmountPromised = 0;
                ChoosePirate = 0;
            }
        }
        class Cupidity : Sovereigns
        {
            public Cupidity()
            {
                Name = "Queen Cupidity";
                Gold = 50;
                AmountPromised = 0;
                ChoosePirate = 0;
            }
        }
        class Rapacity : Sovereigns
        {
            public Rapacity()
            {
                Name = "King Rapacity";
                Gold = 50;
                AmountPromised = 0;
                ChoosePirate = 0;
            }
        }
    }
