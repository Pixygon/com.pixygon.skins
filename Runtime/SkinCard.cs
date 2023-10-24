using Pixygon.Core;
using Pixygon.NFT;
using UnityEngine;

namespace Pixygon.Skins {
    [CreateAssetMenu(menuName = "Pixygon/New SkinCard")]
    public class SkinCard : ScriptableObject {
        public string _title;
        public string _collabPartner;
        public int _maxSupply;
        public int _price;
        public string _cardNumber;
        public Texture2D _collabIcon;
        public Texture2D _gameIcon;
        public Texture2D _gameBg;
        public Sprite _skinSprite;
        [ContextMenuItem("Set rarity", "SetRarity")]
        public Rarity _rarity;
        public string _purchaseUrl;
        public NFTLink _nftLink;

        private void SetRarity() {
            _rarity = _maxSupply switch {
                1 => Rarity.Mythical,
                >= 1 and <= 10 => Rarity.Legendary,
                >= 10 and <= 50 => Rarity.Epic,
                >= 50 and <= 500 => Rarity.Rare,
                >= 500 and <= 1000 => Rarity.Scarce,
                >= 1000 and <= 10000 => Rarity.Common,
                >= 10000 => Rarity.Infinite,
                _ => _rarity
            };
        }
    }
}