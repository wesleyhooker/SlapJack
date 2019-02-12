using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlapJack
{
    public enum Face {Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace};
    
    class Card
    {
        public string Suit { get; set; }
        public string Face { get; set; }
        private Face face;
       
        

        public Card(string cardSuit, string cardFace)
        {
            Suit = cardSuit;
            Face = cardFace;
        }

        public override string ToString()
        {
            return (Face + "_of_" + Suit + ".png");
        }

        public Face faceValue
        {
            get
            { return face;
            }
            set
            { face = value;
            }

        }
    }
}
