using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.Energy
{
    public class AddExercisePanel : PanelView
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _saveBtn;

        private string _stringValue;

        public event Action<string> OnPressSaveBtnAction;

        private void Awake()
        {
            _inputField.onEndEdit.AddListener(OnEndEditInputField);
            _saveBtn.onClick.AddListener(OnPressSaveBtn);
        }

        private void OnDestroy()
        {
            _inputField.onEndEdit.RemoveAllListeners();
            _saveBtn.onClick.RemoveAllListeners();

            _inputField.text = "";
        }

        private void OnEndEditInputField(string input)
        {
            _saveBtn.interactable = !string.IsNullOrWhiteSpace(input);

            _stringValue = input;
        }

        private void OnPressSaveBtn()
        {
            OnPressSaveBtnAction?.Invoke(_stringValue);
        }
    }
}