using System;
using UnityEngine;

namespace Pixygon.Skins {
    public class SkinMenu : MonoBehaviour {
        [SerializeField] private SkinCard[] _skins;
        [SerializeField] private SkinMenuSlotBase[] _slots;
        [SerializeField] private GameObject _purchaseButton;
        [SerializeField] private GameObject _equipButton;
        private SkinMenuSlotBase CurrentSlot => _slots[2];
        private int _currentSlot;
        private SkinSetter[] _skinSetters;

        public Action<int> _skinSetterAction;
        private bool _useNfts;

        public void Populate(SkinSetter[] skinSetter, bool useNfts = false) {
            gameObject.SetActive(true);
            ClearInventory();
            _skinSetters = skinSetter;
            _currentSlot = 0;
            _slots[2].OnUnlock = SetEquipButton;
            _useNfts = useNfts;
            RefreshInventory();
        }

        private void RefreshInventory() {
            if (_skins == null) return;
            if (_skins.Length == 0) return;
            _slots[0].Set(_currentSlot - 2 < 0 ? null : _skins[_currentSlot - 2]);
            _slots[1].Set(_currentSlot - 1 < 0 ? null : _skins[_currentSlot - 1]);
            _slots[2].Set(_skins[_currentSlot]);
            _slots[3].Set(_currentSlot + 1 >= _skins.Length ? null : _skins[_currentSlot + 1]);
            _slots[4].Set(_currentSlot + 2 >= _skins.Length ? null : _skins[_currentSlot + 2]);
            CurrentSlot.Activate(true);
            SetEquipButton();
        }

        private void SetEquipButton() {
            _equipButton.SetActive(CurrentSlot.IsUnlocked);
            #if UNITY_IOS || UNITY_ANDROID
            _purchaseButton.SetActive(false);
            #else
            _purchaseButton.SetActive(!CurrentSlot.IsUnlocked && _useNfts);
            #endif
        }

        public void Move(int x) {
            if (x == -1) {
                if (_currentSlot == 0) _currentSlot = _skins.Length-1;
                else _currentSlot -= 1;
            } else if (x == 1) {
                if (_currentSlot == _skins.Length -1) _currentSlot = 0;
                else _currentSlot += 1;
            }
            RefreshInventory();
        }

        public void Select() {
            if (CurrentSlot == null) return;
            if (!CurrentSlot.IsUnlocked) {
                Debug.Log("Not owned!! " + _skins[_currentSlot]._title);
                return;
            }
            PlayerPrefs.SetInt("Skin", _currentSlot);
            PlayerPrefs.Save();
            foreach (var s in _skinSetters)
                s.SetSkin(_currentSlot);
        }

        public void OnPurchase() {
            Application.OpenURL($"{_skins[_currentSlot]._purchaseUrl}");
        }
        public void ClearInventory() {
            for (var i = 0; i < _slots.Length; i++) {
                _slots[i].Set(null);
            }
            CurrentSlot.Activate(false);
            _currentSlot = 0;
            CurrentSlot.Activate(true);
        }

        public void Close() {
            gameObject.SetActive(false);
        }
    }
}