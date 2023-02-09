namespace GamesClub.Code.Core.MemoryGame.Card
{
    public class CardDeck
    {
        public Card[] Cards { get; private set; }
        public CardView[] CardViews { get; private set; }

        public CardDeck(Card[] cards, CardView[] cardViews)
        {
            Cards = cards;
            CardViews = cardViews;
        }
        
        public Card GetCard(int id) => 
            Cards[id];
    }
}