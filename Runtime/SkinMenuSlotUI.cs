using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pixygon.Micro.Skins {
    public class SkinMenuSlotUI : SkinMenuSlotBase {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _cardIdText;
        [SerializeField] private Image _icon;
        
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