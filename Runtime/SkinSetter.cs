﻿using UnityEngine;

namespace Pixygon.Micro.Skins {
    public class SkinSetter : MonoBehaviour {
        [SerializeField] private Animator _anim;
        [SerializeField] private AnimatorOverrideController[] _anims;

        private bool _skinEnabled;
        private int _enabledSkin = 0;
        private RuntimeAnimatorController _defaultController;
        private void Start() {
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