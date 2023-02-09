using UnityEngine;

namespace GamesClub.Code.Core.MemoryGame.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        public int Id { get; private set; }
        
        public void Construct(int id)
        {
            Id = id;
        }

        public void SetNewSprite(Sprite sprite) => 
            _sprite.sprite = sprite;
    }
}