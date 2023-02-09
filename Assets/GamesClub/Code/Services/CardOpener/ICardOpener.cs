using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamesClub.Code.Core.MemoryGame.Card;

namespace GamesClub.Code.Services.CardOpener
{
    public interface ICardOpener
    {
        bool CheckIsPair(Card prevCard1, Card prevCard2);
        UniTask ShowCardFront(Card card);
        UniTask ShowCardBack(Card card);
    }
}