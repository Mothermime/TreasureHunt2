﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreasureHunt2
{
    public abstract class Shipping
    {
        public string Name { get; set; }
        public string PirateName { get; set; }
        
        public int PathCounter { get; set; }
        public int PiratePathCounter { get; set; }
        public int ChoosePirate { get; set; }
        public abstract List<string> ShipPath { get; set; }
        public abstract List<string> PiratePath { get; set; }
        public PictureBox MyPictureBox = null;
        public PictureBox PiratePb = null;
        public int Location = 0;
        public Random Randomizer;
        public int StartingPosition;
        public bool HasFinished = false;
        public bool HasArrived = false;
        public bool HasSailed = false;
        public PictureBox Piratepb = null;

        public static implicit operator Shipping(PictureBox v)
        {
            throw new NotImplementedException();
        }

        //{
        //string shipdata = ShipPath[PathCounter];
        ////split the path into left and top  
        //string[] moreshipdata = shipdata.Split(',');

        ////attach to picturebox

        ////count through path
        //PathCounter += 1;
        // }
    }
    public interface IShipping
    {
        void SetSail();
        void FindTreasure();
    }

}


