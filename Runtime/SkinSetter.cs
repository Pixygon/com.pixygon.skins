using UnityEngine;

namespace Pixygon.Skins {
    public class SkinSetter : MonoBehaviour {
        [SerializeField] private Animator _anim;
        [SerializeField] private AnimatorOverrideController[] _anims;
        [SerializeField] private bool _nonPlayer;

        private bool _skinEnabled;
        private int _enabledSkin = 0;
        private RuntimeAnimatorController _defaultController;
        private void Start() {
            if (_nonPlayer) return;
            _defaultController = _anim.runtimeAnimatorController;
            _enabledSkin = PlayerPrefs.GetInt("Skin", 0);
            SetSkin(_enabledSkin);
        }

        public void SetSkin(int id) {
            _skinEnabled = id != 0;
            _anim.runtimeAnimatorController = _skinEnabled == false ? _defaultController : _anims[id - 1];
        }
    }
}