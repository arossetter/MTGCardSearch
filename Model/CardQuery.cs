using System;
using System.Collections.Generic;
using System.Text;

namespace MTGCardSearch.Model
{
    public class CardQuery
    {
        public enum CardType
        {
            Artifact,
            Conspiracy,
            Creature,
            Enchantment,
            Instant,
            Land,
            Phenomenon,
            Plane,
            Planeswalker,
            Scheme,
            Sorcery,
            Tribal,
            Vanguard
        }

        public string cardName;

        public bool isWhite = false;
        public bool isBlue = false;
        public bool isBlack = false;
        public bool isRed = false;
        public bool isGreen = false;

        public string cardType = "";

        public string cmc = "";
    }
}
