using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreasureHunt2.Properties;

namespace TreasureHunt2
{

    public partial class Form1 : Form, IShipping
    {

        private RadioButton rbChosenSovereign;
        private RadioButton rbChosenPirate;

        private Shipping[] Ship = new Shipping[4];
        private Shipping[] Pirate = new Shipping[4];
        private Random myRandom = new Random();

        public bool End = false;
        private int PathCounter = 0;
        private int PiratePathCounter = 0;
        private int Winner = 5;
        private Sovereigns[] Investor = new Sovereigns[3];

        private RadioButton rbInvestor;
        private RadioButton rbPirate;
        //private Shipping[] myShipping;

        // private int shipsLand = 0;
        // private int piratesArrive = 0;

        public Form1()
        {//Instantiate everything
            //Set up all the different components
            InitializeComponent();
            Ship[0] = new Sapphire();
            Ship[1] = new Ruby();
            Ship[2] = new Emerald();
            Ship[3] = new Topaz();
            Ship[0].MyPictureBox = pbBlue;
            Ship[1].MyPictureBox = pbRed;
            Ship[2].MyPictureBox = pbGreen;
            Ship[3].MyPictureBox = pbYellow;
            Ship[0].PiratePb = pbBluebeard;
            Ship[1].PiratePb = pbRedCoat;
            Ship[2].PiratePb = pbGreenThumb;
            Ship[3].PiratePb = pbYellowBelly;
            SetSovereigns();

            //Apparently I don't need these after all
            //Pirate[0] = new Sapphire();
            //Pirate[1] = new Ruby();
            //Pirate[2] = new Emerald();
            //Pirate[3] = new Topaz();

        }
        public void SetSovereigns()
        {
            Investor[0] = new Esurience();
            Investor[1] = new Cupidity();
            Investor[2] = new Rapacity();
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {//change instructions, reveal and hide buttons
            gbinvest.Visible = true;
            tbInstructions.Visible = false;
            btnLoadShips.Visible = true;
            tbResults.Visible = true;
            btnInvest.Visible = true;
            btnPlay.Visible = false;
            btnGameOver.Visible = false;
            gbPiratelabels.Visible = true;
        }

     
        private void rbInvestor_CheckedChanged(object sender, EventArgs e)
        {//assign all radio buttons to one click event which will change which one is checked
            //but also assign all button click events to rbInvestor_CheckedChanged as well so one will work if two are disabled
            rbInvestor = (RadioButton) sender;
            if (rbInvestor.Checked == true)
            {
                Factory.ChooseSovereign(rbInvestor.Text);
                //show how much the selected Sovereign has available to inves
                lblGoldHoard.Text = Convert.ToString(Investor[Factory.BackerNumber].Gold);
                nudInvestment.Maximum = Investor[Factory.BackerNumber].Gold;
            }
            if (rbInvestor.Enabled == false)
            {
                Investor[Factory.BackerNumber].MyTextBox.Text = "Ruined";
            }
        }
        private void btnInvest_Click(object sender, EventArgs e)
        {//both up/down boxes need to match the investor.  
            Investor[Factory.BackerNumber].AmountPromised = Convert.ToInt32(nudInvestment.Value);

            Investor[Factory.BackerNumber].ChoosePirate = Convert.ToInt32(nudPirate.Value);

            //specify what happens in each individual case
            //would a for loop work here???
            if (Investor[Factory.BackerNumber].Name == "King Esurience")
            {
                txtEsurience.Text = Investor[Factory.BackerNumber].Name + " has invested " +
                                    Investor[Factory.BackerNumber].AmountPromised + " hoards with Pirate " +
                                    Investor[Factory.BackerNumber].ChoosePirate; 
            }
            else if (Investor[Factory.BackerNumber].Name == "Queen Cupidity")
            {
                txtCupidity.Text = Investor[Factory.BackerNumber].Name + " has invested " +
                                   Investor[Factory.BackerNumber].AmountPromised + " hoards with Pirate " +
                                   Investor[Factory.BackerNumber].ChoosePirate;
            }
            else if (Investor[Factory.BackerNumber].Name == "King Rapacity")
            {
                txtRapacity.Text = Investor[Factory.BackerNumber].Name + " has invested " +
                                   Investor[Factory.BackerNumber].AmountPromised + " hoards with Pirate " +
                                   Investor[Factory.BackerNumber].ChoosePirate; 
            }

        }
        private void btnLoadShips_Click(object sender, EventArgs e)
        {//make sure the button is visible for reruns
            btnSetSail.Visible = true;
            //as long as the investors have invested at least once, the instructions disappear and the race appears
            if (txtEsurience.Text != string.Empty && txtCupidity.Text != String.Empty && txtRapacity.Text != String.Empty)
            {
                gbBackers.Visible = false;
            }
            else
            {//if there's an empty textbox show the message
                MessageBox.Show("Not all Sovereigns have invested.");
            }
        }
        private void LoadShips()
        {//make 'end' false so that set sail() will work again
            End = false;
            //reset ships and pirates to beginning positions
            pbBlue.Left = 1216;
            pbBlue.Top = -1;
            pbBlue.Left = 1216;
            pbBlue.Top = -1;
            pbRed.Left = 1216;
            pbRed.Top = 675;
            pbGreen.Left = -1;
            pbGreen.Top = 675;
            pbYellow.Left = -1;
            pbYellow.Top = -1;
            pbBluebeard.Left = 748;
            pbBluebeard.Top = 337;
            pbRedCoat.Left = 710;
            pbRedCoat.Top = 462;
            pbGreenThumb.Left = 655;
            pbGreenThumb.Top = 437;
            pbYellowBelly.Left = 553;
            pbYellowBelly.Top = 264;
            //reset counters for everything
            for (int i = 0; i < 4; i++)
            {
                Ship[i].PathCounter = 0;
                Ship[i].PiratePb.Visible = false;
                Ship[i].PiratePathCounter = 0;
                Ship[i].HasFinished = false;
            }
            Text = "";
        }

        private void btnSetSail_Click(object sender, EventArgs e)
        {
            btnSetSail.Visible = false;
            //tbResults.Visible = false;
            //tbWinner.Visible = false;
            //get the button out of the way

            //ALL MANNER OF TRYING TO GET IT TO WORK/STOP WORKING,  TESTING AND RETESTING

            // while (End == false)

            ////do
            // {
            SetSail();
            //} //while (Ship[0].HasArrived == false || Ship[1].HasArrived == false || Ship[2].HasArrived == false ||
            //      Ship[3].HasArrived == false || End == false);
            //(End == false)
            //do
            //{
            //    FindTreasure();

            //} //while (End == false);
            //while (Ship[0].HasArrived == false || Ship[1].HasArrived == false || Ship[2].HasArrived == false || Ship[3].HasArrived == false);
            //WinningPirate();
        }
        public void SetSail()
        {
            //finally found the right place to put the while statement after lots of different locations
            while (End == false)
            {
                //for each ship
                for (int i = 0; i < 4; i++)
                {
                    //Make it run
                    Application.DoEvents();
                    //Slow down a bit
                    System.Threading.Thread.Sleep(10);
                    //split the path array for each ship into left [0] and top [1] points

                    string path = Ship[i].ShipPath[Ship[i].PathCounter];
                    string[] pathArray = path.Split(',');
                    Ship[i].MyPictureBox.Left = Convert.ToInt16(pathArray[0]);
                    Ship[i].MyPictureBox.Top = Convert.ToInt16(pathArray[1]);

                    if (Ship[i].PathCounter >= 48)
                    {
                        //keep looping through path until each ship has reached 49 steps
                        //Ship[i].PathCounter = 49;
                        Ship[i].HasFinished = true;
                        Ship[i].PiratePb.Visible = true;
                    }
                    if (Ship[i].HasFinished)
                    //when each ship finishes start the pirate along his path
                    {
                        //same as ship activate, slow it down
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(250);
                        string piratepath = Ship[i].PiratePath[Ship[i].PiratePathCounter];
                        string[] piratepathArray = piratepath.Split(',');
                        Ship[i].PiratePb.Left = Convert.ToInt16(piratepathArray[0]);
                        Ship[i].PiratePb.Top = Convert.ToInt16(piratepathArray[1]);
                        {
                            if (Ship[i].PiratePathCounter <= 10)
                            {
                                //randomize pirates
                                Ship[i].PiratePathCounter += myRandom.Next(0, 2);
                            }
                            else if (Ship[i].PiratePathCounter >= 12)
                            {
                                //once the pirate reaches the treasure the race is over
                                End = true;
                                //add one to the ship[] number because arrays start with 0
                                Winner = i + 1;
                                //display winner on the top of the form
                                //  Text= Winners.PirateNameToNumber.FirstOrDefault(x => x.Value == i + 1).Key;
                                Text = Ship[i].PirateName;
                                //Text = Convert.ToString(Winners.Winner);
                                //show the winner and the button to return to the soveriegns
                                btnReturn.Visible = true;
                                tbWinner.Visible = true;
                                //put the appropriate name n the winning announcement
                                tbWinner.Text = Ship[i].PirateName + " " + "has found the treasure!";

                                ////if (Ship[0].HasArrived == true && Ship[1].HasArrived == true &&
                                ////    Ship[2].HasArrived == true && Ship[3].HasArrived == true)

                                ////{
                                ////    pbBattle.Visible = true;
                                ////    Ship[i].PiratePb.Visible = false;

                                //// Finish!!! but they wouldn't stop

                            } //add one step each time it loops through
                        }
                        Ship[i].PiratePathCounter += 1;
                    }
                    else
                    {
                        //move the ship a random distance along the path
                        Ship[i].PathCounter += myRandom.Next(0, 2);
                    }
                }

                //make a counter to count through the list
                PathCounter += 1;
                // EndFight();
                SetSail();
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            rbInvestor.Checked = false;
            //bring back the view for investing and set it for rerun
            gbBackers.Visible = true;
            lblGoldHoard.Text = " ";
            //hide the winner information and button from the race view
            tbWinner.Visible = false;
            btnReturn.Visible = false;

            //run the methods 
            WinningCalc(Winner);
            LoadShips();

            //make sure end is false so that the set sail method will run again
           // End = false;
        }
        public void WinningCalc(int Winner)
        {//loop through them all
            for (int i = 0; i < 3; i++)
            {
                if (Winner == Investor[i].ChoosePirate)
                {
                    //if the investor has chosen the winning pirate, add the amount invested to the total of their wealth
                    Investor[i].Gold += Investor[i].AmountPromised;
                    //Show in their text box that they have won and by means of which pirate
                    Investor[i].MyTextBox.Text = Investor[i].Name + " received " + Investor[i].AmountPromised +
                                                 " pieces of treasure from Pirate " + Investor[i].ChoosePirate;
                }
                else // (Winner != Investor[i].ChoosePirate)
                {
//if the winning pirate was not chosen, subtract the amount invested fromm their total
                    Investor[i].Gold -= Investor[i].AmountPromised;
                    //and show in the text box
                    Investor[i].MyTextBox.Text = (Investor[i].Name + " lost " + Investor[i].AmountPromised +
                                                  " hoards because of Pirate " + Winner + "'s success.");

                }
                if (Investor[i].Gold <= 0)
                    {//if they have nothing left - say so!
                        Investor[i].MyTextBox.Text = Investor[i].Name + " is ruined!";
                  
                        //reveal finish picture
                        if (Investor[i] == Investor[0])
                        {
                        rbEsurience.Enabled = false;
                            pbETreasureChest.Visible = true;
                        }
                        else if (Investor[i] == Investor[1])
                        {
                        rbCupidity.Enabled = false;
                            pbCTreasureChest.Visible = true;
                        }
                        else if (Investor[i] == Investor[2])
                        {
                        rbRapacity.Enabled = false;
                            pbRTreasureChest.Visible = true;
                        }
                        //Investor[i].MyTextBox.Enabled = false;                    
                    }
            }
            //specify the different text boxes for each investor
            txtEsurience.Text = Investor[0].MyTextBox.Text;
                txtCupidity.Text = Investor[1].MyTextBox.Text;
            txtRapacity.Text = Investor[2].MyTextBox.Text;
            NewGame();
        }                             
      

       
        private void NewGame()
        {//if all of the radio buttons are disabled show that the game is over but sart resetting.
            if (rbEsurience.Enabled == false && rbCupidity.Enabled == false && rbRapacity.Enabled == false)
            {
                gbinvest.Visible = false;
                btnGameOver.Visible = true;
                LoadShips();                     
            }
        }
        private void btnGameOver_Click(object sender, EventArgs e)
        {//Reset all the different components to their beginning state
            SetSovereigns();
            btnGameOver.Visible = false;
            tbInstructions.Visible = true;
            gbPiratelabels.Visible = false;
            btnPlay.Visible = true;
                pbETreasureChest.Visible = false;
            pbCTreasureChest.Visible = false;
            pbRTreasureChest.Visible = false;
            // Investor[Factory.BackerNumber].Gold = 50;
            //for (int i = 0; i < 3; i++)
            //{
            //    Investor[i].MyTextBox.Text = " ";
            //   // rbInvestor.Enabled = true;
            //}
            txtEsurience.Text = " ";
            txtCupidity.Text = " ";
            txtRapacity.Text = " ";
            rbRapacity.Enabled = true;
            rbEsurience.Enabled = true;
            rbCupidity.Enabled = true;
        }

       
        // ===========================================================================================//
        //                UNUSED CODE

        //private static string WinningPirate()

        //{
        //    int Pirate = 1;
        //    string Name = "";
        //    switch (Pirate)
        //    {

        //        case 1:
        //            Name = " Capt. Bluebeard";
        //            break;
        //        case 2:

        //            Name = "Cmdr. Redcoat";
        //            break;
        //        case 3:
        //            Name = "Capt. Emerald";

        //            break;
        //        case 4:
        //            Name = "Cmdr. Topaz ";
        //            break;
        //    }
        //    return Name;
        //}

        /////// TEsting pirate path counter
        //public
        //    void FindTreasure()
        //{
        //    do
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            Ship[i].PiratePb.Visible = true;
        //            Application.DoEvents();
        //            System.Threading.Thread.Sleep(100);
        //            string piratepath = Ship[i].PiratePath[Ship[i].PiratePathCounter];
        //            string[] piratepathArray = piratepath.Split(',');
        //            Ship[i].PiratePb.Left = Convert.ToInt16(piratepathArray[0]);
        //            Ship[i].PiratePb.Top = Convert.ToInt16(piratepathArray[1]);
        //            if (Ship[i].PiratePathCounter <= 10)
        //            {
        //                Ship[i].PiratePathCounter += myRandom.Next(0, 2);
        //            }
        //            else if (Ship[i].PiratePathCounter >= 12)
        //            {
        //                Ship[i].PiratePathCounter = 12;
        //                End = true;
        //                Ship[i].HasArrived = true;
        //                Winner = i + 1;
        //                Text = Convert.ToString(Winner);
        //            }
        //            Ship[i].PiratePathCounter += 1;
        //        }
        //    } while (End == false);

        //}



        //private void gbinvest_Enter(object sender, EventArgs e)
        //{

        //}



        //        //switch (Name)
        //        //{

        //        //    case "Sapphire":
        //        //        // pbBluebeard.Visible = true;
        //        //        BluebeardTrack();
        //        //        //run method that makes pirate for that ship run
        //        //        break;
        //        //    case "Ruby":
        //        //        // pbRedCoat.Visible = true;
        //        //        RedCoatTrack();
        //        //        break;
        //        //    case "Emerald":

        //        //        //pbGreenThumb.Visible = true;
        //        //        GreenThumbTrack();
        //        //        break;
        //        //    case "Topaz":
        //        //        // pbYellowBelly.Visible = true;
        //        //        YellowBellyTrack();
        //        //        break;


        //        //};





        //    }




        //    public void BluebeardTrack()
        //    {
        //        Application.DoEvents();
        //        System.Threading.Thread.Sleep(200);
        //        string Bluebeard = Ship[0].PiratePath[PiratePathCounter];
        //        string[] Bluebearddata = Bluebeard.Split(',');
        //        pbBluebeard.Left = Convert.ToInt16(Bluebearddata[0]);
        //        pbBluebeard.Top = Convert.ToInt16(Bluebearddata[1]);

        //    }

        //    public void RedCoatTrack()
        //    {
        //        Application.DoEvents();
        //        System.Threading.Thread.Sleep(200);
        //        string RedCoat = Ship[1].PiratePath[PiratePathCounter];
        //        string[] RedCoatdata = RedCoat.Split(',');
        //        pbRedCoat.Left = Convert.ToInt16(RedCoatdata[0]);
        //        pbRedCoat.Top = Convert.ToInt16(RedCoatdata[1]);

        //    }

        //    public void GreenThumbTrack()
        //    {
        //        Application.DoEvents();
        //        System.Threading.Thread.Sleep(200);
        //        string GreenThumb = Ship[2].PiratePath[PiratePathCounter];
        //        string[] GreenThumbdata = GreenThumb.Split(',');
        //        pbGreenThumb.Left = Convert.ToInt16(GreenThumbdata[0]);
        //        pbGreenThumb.Top = Convert.ToInt16(GreenThumbdata[1]);

        //    }

        //    public void YellowBellyTrack()
        //    {
        //        Application.DoEvents();
        //        System.Threading.Thread.Sleep(200);
        //        string YellowBelly = Ship[3].PiratePath[PiratePathCounter];
        //        string[] YellowBellydata = YellowBelly.Split(',');
        //        pbYellowBelly.Left = Convert.ToInt16(YellowBellydata[0]);
        //        pbYellowBelly.Top = Convert.ToInt16(YellowBellydata[1]);

        //    }

    }
}
//Trying out how to get the pictures to move diagonally first
//Slow them down
//Application.DoEvents();

//{//Moving in a spiral. Need more points to make it smoother
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(500);
//    pbBlue.Left = 1036;
//    pbBlue.Top = 22;
//    pbRed.Left = 1159;
//    pbRed.Top = 552;
//    pbGreen.Left = 174;
//    pbGreen.Top = 649;
//    pbYellow.Left = 48;
//    pbYellow.Top = 164;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 811;
//    pbBlue.Top = 48;
//    pbYellow.Left = 100;
//    pbYellow.Top = 344;
//    pbRed.Left = 1187;
//    pbRed.Top = 552;
//    pbGreen.Left = 390
//    pbGreen.Top = 624;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 562;
//    pbBlue.Top = 78;
//    pbYellow.Left = 187;
//    pbYellow.Top = 511;
//    pbRed.Left = 1051;
//    pbRed.Top = 280;
//    pbGreen.Left = 617;
//    pbGreen.Top = 594;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 358;
//    pbBlue.Top = 132;
//    pbYellow.Left = 339;
//    pbYellow.Top = 624;
//    pbRed.Left = 969;
//    pbRed.Top = 164;
//    pbGreen.Left = 791;
//    pbGreen.Top = 552;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 222;
//    pbBlue.Top = 235;
//    pbYellow.Left = 526;
//    pbYellow.Top = 624;
//    pbRed.Left = 842;
//    pbRed.Top = 91;
//    pbGreen.Left = 935;
//    pbGreen.Top = 474;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 209;
//    pbBlue.Top = 384;
//    pbYellow.Left = 721;
//    pbYellow.Top = 594;
//    pbRed.Left = 663;
//    pbRed.Top = 67;
//    pbGreen.Left = 969;
//    pbGreen.Top = 344;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 305;
//    pbBlue.Top = 501;
//    pbYellow.Left = 885;
//    pbYellow.Top = 511;
//    pbRed.Left = 488;
//    pbRed.Top = 152;
//    pbGreen.Left = 899;
//    pbGreen.Top = 235;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 526;
//    pbBlue.Top = 511;
//    pbYellow.Left = 935;
//    pbYellow.Top = 384;
//    pbRed.Left = 390;
//    pbRed.Top = 247;
//    pbGreen.Left = 769;
//    pbGreen.Top = 153;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 706;
//    pbBlue.Top = 474;
//    pbYellow.Left = 885;
//    pbYellow.Top = 258;
//    pbRed.Left = 358;
//    pbRed.Top = 365;
//    pbGreen.Left = 617;
//    pbGreen.Top = 164;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 811;
//    pbBlue.Top = 384;
//    pbYellow.Left = 757;
//    pbYellow.Top = 201;
//    pbRed.Left = 432;
//    pbRed.Top = 450;
//    pbGreen.Left = 500;
//    pbGreen.Top = 281;

//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 757;
//    pbBlue.Top = 280;
//    pbYellow.Left = 604;
//    pbYellow.Top = 234;
//    pbRed.Left = 562;
//    pbRed.Top = 462;
//    pbGreen.Left = 478;
//    pbGreen.Top = 320;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);
//    pbBlue.Left = 650;
//    pbBlue.Top = 290;
//    pbYellow.Left = 562;
//    pbYellow.Top = 290;
//    pbRed.Left = 650;
//    pbRed.Top = 387;
//    pbGreen.Left = 562;
//    pbGreen.Top = 387;


//PathGeometry animationPath = new PathGeometry();
//PathFigure pFigure = new PathFigure();
//pFigure.StartPoint = new Point(10, 100);
//PolyBezierSegment pBezierSegment = new PolyBezierSegment();
//pBezierSegment.Points.Add(new Point(35, 0));
//pBezierSegment.Points.Add(new Point(135, 0));
//pBezierSegment.Points.Add(new Point(160, 100));
//pBezierSegment.Points.Add(new Point(180, 190));
//pBezierSegment.Points.Add(new Point(285, 200));
//pBezierSegment.Points.Add(new Point(310, 100));
//pFigure.Segments.Add(pBezierSegment);
//animationPath.Figures.Add(pFigure);



//private void bluesail()

//{
// List<string> ShipPath = new List<string>();

//ShipPath.Add("1217, 0""1126,33" );
//ShipPath.Add("1122,18"):
//string[] anotherpath = ShipPath[0].Split(',');
//int Left = Convert.ToInt32(anotherpath[0]);
//int top = Convert.ToInt32(anotherpath[1]);


//int ShipLastMoveCount = 0;
//int DiceRoll = 5;

//for (int i = ShipLastMoveCount; i < DiceRoll; i++)
//{
//    string[] path = ShipPath[i].Split(',');
//    int Lefta = Convert.ToInt32(path[0]);
//    int topa = Convert.ToInt32(path[1]);


//    pbGreen.Left = Lefta;
//    pbGreen.Top = topa;
//    Application.DoEvents();
//    System.Threading.Thread.Sleep(1000);


// }

//ShipLastMoveCount += ShipLastMoveCount + DiceRoll;

//{
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 1128;
//        pbBlue.Top = 12;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 1036;
//        pbBlue.Top = 22;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 918;
//        pbBlue.Top = 35;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 811;
//        pbBlue.Top = 48;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 685;
//        pbBlue.Top = 61;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 570;
//        pbBlue.Top = 78;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 452;
//        pbBlue.Top = 101;

//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 358;
//        pbBlue.Top = 132;
//        Application.DoEvents();

//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 222;
//        pbBlue.Top = 235;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 209;
//        pbBlue.Top = 384;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 305;
//        pbBlue.Top = 501;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 526;
//        pbBlue.Top = 511;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 706;
//        pbBlue.Top = 474;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 809;
//        pbBlue.Top = 384;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 757;
//        pbBlue.Top = 280;
//        Application.DoEvents();
//        System.Threading.Thread.Sleep(300);
//        pbBlue.Left = 650;
//        pbBlue.Top = 290;

//    }


//private void pbYellow_Click(object sender, EventArgs e)
//{

//}

//private void pbGreen_Click(object sender, EventArgs e)
//{

//}

//private void Form1_Load(object sender, EventArgs e)
//{

//}

//    private void button2_Click(object sender, EventArgs e)
//    {
//        bluesail();












