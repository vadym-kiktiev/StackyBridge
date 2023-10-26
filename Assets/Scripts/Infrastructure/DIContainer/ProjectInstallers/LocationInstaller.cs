using Infrastructure.DIContainer.Extensions;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer.ProjectInstallers
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _keyboardController;
        [SerializeField] private GameObject _swipeController;
        [SerializeField] private GameObject _joystickInputService;
        [SerializeField] private GameObject _touchInputService;

        public override void InstallBindings()
        {
            Debug.Log("LocationInstaller.Initialize()");

            AttachInput();
        }

        private void AttachInput()
        {
            switch (InputConfig.InputType)
            {
                case InputType.Swipe:
                    Container.BindService<IInputService, SwipeController>(_swipeController);
                    break;
                case InputType.Touch:
                    Container.BindService<IInputService,TouchInputService>(_touchInputService);
                    break;
                case InputType.Joystick:
                    Container.BindService<IInputService, JoystickInputService>(_joystickInputService);
                    break;
            }
        }
    }
}
