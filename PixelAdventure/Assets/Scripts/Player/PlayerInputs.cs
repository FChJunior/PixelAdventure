using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float Movement()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        return movement;
    }
    public float MovementIce()
    {
        float movement = Input.GetAxis("Horizontal");
        return movement;
    }
    public bool Jump()
    {
        bool jump = Input.GetButtonDown("Jump");
        return jump;
    }

    public bool Dash()
    {
        bool dash = Input.GetKeyDown(KeyCode.J);
        return dash;
    }

    public float DirectionDash()
    {
        float dir = Input.GetAxisRaw("Vertical");
        return dir;
    }
}
