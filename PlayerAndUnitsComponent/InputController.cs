using UnityEngine;
using System.Collections.Generic;
//This controller is less meant to check if a input is pressed but to press inputs . that can than be ckecked by other scripts
public class InputController : MonoBehaviour
{
    bool leftPressed;
    bool rightPressed;
    bool upPressed;
    bool downPressed;
    bool jumpPressed;
    bool attackPressed;
    
//Update
  
    
    public bool getLeftPressed()
    {
        return leftPressed;
    }
    public bool getRightPressed()
    {
        return rightPressed;
    }
    public bool getUpPressed()
    {
        return upPressed;
    }
    public bool getDownPressed()
    {
        return downPressed;
    }
    public bool getJumpPressed()
    {
        return jumpPressed;
    }
    public bool getAttackPressed()
    {
        return attackPressed;
    }
    public void setLeftPressed(bool pressed)
    {
        leftPressed = pressed;
    }
    public void setRightPressed(bool pressed)
    {
        rightPressed = pressed;
    }
    public void setUpPressed(bool pressed)
    {
        upPressed = pressed;
    }

    public void setDownPressed(bool pressed)
    {
        downPressed = pressed;
    }
    public void setJumpPressed(bool pressed)
    {
        jumpPressed = pressed;
    }
    public void setAttackPressed(bool pressed)
    {
        attackPressed = pressed;
    }
}
