using System;
using UnityEngine;

namespace Pixygon.Skins {
    public class SkinMenuSlotBase : MonoBehaviour {
        [SerializeField] protected GameObject _activeFrame;
        [SerializeField] protected GameObject _lockObject;
        [SerializeField] protected bool _simpleSetup;
        public bool IsUnlocked { get; protected set; }
        public SkinCard Skin { get; protected set; }
        public Action OnUnlock;
        protected string _currentTemplate;
        
        public virtual void Set(SkinCard skin) { }
        public void Activate(bool active) {
            _activeFrame.SetActive(active);
        }
        protected void CheckNft() {
            IsUnlocked = false;
            _lockObject.SetActive(true);
            if(!Skin._nftLink.RequiresNFT) {
                IsUnlocked = true;
                _lockObject.SetActive(false);
                return;
            }
            _currentTemplate = Skin._nftLink.Template[0].template.ToString();
            NFT.NFT.ValidateTemplate(Skin._nftLink.Template[0], Validate, null,  Skin._nftLink.Template[0].template.ToString());
        }
        protected void Validate(string id) {
            if (_currentTemplate != id) return;
            IsUnlocked = true; 
            _lockObject.SetActive(false);
            OnUnlock?.Invoke();
        }
    }
}