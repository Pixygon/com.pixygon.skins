using TMPro;
using UnityEngine;

namespace Pixygon.Skins {
    public class SkinMenuSlot : SkinMenuSlotBase {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _cardIdText;
        [SerializeField] private SpriteRenderer _icon;
        public override void Set(SkinCard skin) {
            _text.text = "";
            _icon.sprite = null;
            Skin = skin;
            gameObject.SetActive(Skin != null);
            if (Skin == null) return;
            _icon.sprite = Skin._skinSprite;
            if (_simpleSetup) return;
            _text.text = Skin._title;
            _cardIdText.text = Skin._cardNumber;
            CheckNft();
        }
    }
}