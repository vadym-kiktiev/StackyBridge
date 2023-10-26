using UnityEngine;

public class bl_ControllerExample : MonoBehaviour {

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	[SerializeField]private bl_Joystick Joystick;//Joystick reference for assign in inspector

    [SerializeField]private float Speed = 5;

    void Update()
    {
        float v = Joystick.Vertical;
        float h = Joystick.Horizontal;

        Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
        transform.Translate(translate);
    }
}
