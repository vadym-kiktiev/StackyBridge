using Infrastructure.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] TMP_InputField _playerSpeed;
        [SerializeField] TMP_InputField _redSpeed;
        [SerializeField] TMP_InputField _yellowSpeed;

        [SerializeField] Toggle _swipeInput;
        [SerializeField] Toggle _touchInput;
        [SerializeField] Toggle _joystickInput;

        private void Start()
        {
            _playerSpeed.onValueChanged.AddListener(SetPlayerSpeed);
            _redSpeed.onValueChanged.AddListener(SetRedSpeed);
            _yellowSpeed.onValueChanged.AddListener(SetYellowSpeed);

            _playerSpeed.text = SpeedConfig.PlayerSpeed.ToString();
            _redSpeed.text = SpeedConfig.RedSpeed.ToString();
            _yellowSpeed.text = SpeedConfig.YellowSpeed.ToString();

            Debug.Log(InputConfig.InputType);

            switch (InputConfig.InputType)
            {
                case InputType.Swipe:
                    _swipeInput.SetIsOnWithoutNotify(true);
                    break;
                case InputType.Touch:
                    _touchInput.SetIsOnWithoutNotify(true);
                    break;
                case InputType.Joystick:
                    _joystickInput.SetIsOnWithoutNotify(true);
                    break;
            }
        }

        private void OnDestroy()
        {
            _playerSpeed.onValueChanged.RemoveListener(SetPlayerSpeed);
            _redSpeed.onValueChanged.RemoveListener(SetRedSpeed);
            _yellowSpeed.onValueChanged.RemoveListener(SetYellowSpeed);
        }

        public void SetSwipeInput(bool value)
        {
            if (value)
                InputConfig.InputType = InputType.Swipe;
        }

        public void SetTouchInput(bool value)
        {
            if (value)
                InputConfig.InputType = InputType.Touch;
        }

        public void SetJoystickInput(bool value)
        {
            if (value)
                InputConfig.InputType = InputType.Joystick;
        }

        private void SetPlayerSpeed(string value)
        {
            if(int.TryParse(value, out int speed))
                SpeedConfig.PlayerSpeed = speed;
        }

        private void SetRedSpeed(string value)
        {
            if(int.TryParse(value, out int speed))
                SpeedConfig.RedSpeed = speed;
        }

        private void SetYellowSpeed(string value)
        {
            if(int.TryParse(value, out int speed))
                SpeedConfig.YellowSpeed = speed;
        }
    }
}
