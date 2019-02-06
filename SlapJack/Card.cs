using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlapJack
{
    class Card
    {
        public string Suit { get; set; }
        public string Face { get; set; }

        public Card(string cardSuit, string cardFace)
        {
            Suit = cardSuit;
            Face = cardFace;
        }

        public override string ToString()
        {
            return (Face + "_of_" + Suit + ".png");
        }
    }
}
