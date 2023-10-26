namespace Infrastructure.Services.Input
{
    public enum InputType
    {
        Swipe,
        Touch,
        Joystick
    }

    public static class InputConfig
    {
        public static InputType InputType = InputType.Touch;
    }
}
