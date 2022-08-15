using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Web;

namespace AsteroidStrike
{
    [Serializable()]
    class Controller
    {
        public MouseState mousePosition;

        KeyboardState keyboard;
        KeyboardState preKeyboard;
        Keys[] keysPressed;
        Keys[] prevPressedKeys;

        public bool MouseClickFrame;// is this the frame the mouse button was clicked?
        private bool MouseClicked = false;//is the mouse button currently clicked?

        public bool upPressedFrame;//checks if up was pressed this frame
        private bool upPressed = false;//checks if up is pressed in

        public bool downPressedFrame;//checks if down was pressed this frame
        private bool downPressed= false;//checks if down is currently Pressed

        public bool enterPressedFrame;
        private bool enterPressed = false;

        public Controller()
        {
            keysPressed = keyboard.GetPressedKeys();
        }

        


        public void Update()
        {
            mousePosition = Mouse.GetState();
            preKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (mousePosition.LeftButton == ButtonState.Pressed)//if the button is pressed
            {
                if (!MouseClicked)//if the mouse isn't already clicked
                {
                    MouseClickFrame = true;///it was clicked this frame
                }
                else//otherwise
                {
                    MouseClickFrame = false;// it wasn't clicked this frame
                }
                MouseClicked = true;//the mouse button is down either way
            }
            else// if the mouse button isn't pressed
            {
                MouseClicked = false;//flip the variable
            }
            #region space
            if (keyboard.IsKeyDown(Keys.Enter))//if the button is pressed
            {
                if (!enterPressed)//if the mouse isn't already clicked
                {
                    enterPressedFrame = true;///it was clicked this frame
                }
                else//otherwise
                {
                    enterPressedFrame = false;// it wasn't clicked this frame
                }
                enterPressed = true;//the mouse button is down either way
            }
            else// if the mouse button isn't pressed
            {
                enterPressed = false;//flip the variable
            }
            #endregion
            #region Up
            if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W))//if the button is pressed
            {
                if (!upPressed)//if the mouse isn't already clicked
                {
                    upPressedFrame = true;///it was clicked this frame
                }
                else//otherwise
                {
                    upPressedFrame = false;// it wasn't clicked this frame
                }
                upPressed = true;//the mouse button is down either way
            }
            else// if the mouse button isn't pressed
            {
                upPressed = false;//flip the variable
            }
            #endregion
            #region Down
            if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S))//if the button is pressed
            {
                if (!downPressed)//if the mouse isn't already clicked
                {
                    downPressedFrame = true;///it was clicked this frame
                }
                else//otherwise
                {
                    downPressedFrame = false;// it wasn't clicked this frame
                }
                downPressed = true;//the down button is down either way
            }
            else// if the down button isn't pressed
            {
                downPressed = false;//flip the variable
            }
            #endregion
        }
        #region Keyboard

        public string KeyboardInputString(string myString)
        {
            string output = myString;
            string input = "";
            char[] MyChar = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            if (keyboard.IsKeyDown(Keys.A))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.A))))
                {
                    input = "A";
                }
            }
            if (keyboard.IsKeyDown(Keys.B))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.B))))
                {
                    input = "B";
                }
            }
            if (keyboard.IsKeyDown(Keys.C))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.C))))
                {
                    input = "C";
                }
            }
            if (keyboard.IsKeyDown(Keys.D))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D))))
                {
                    input = "D";
                }
            }
            if (keyboard.IsKeyDown(Keys.E))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.E))))
                {
                    input = "E";
                }
            }
            if (keyboard.IsKeyDown(Keys.F))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.F))))
                {
                    input = "F";
                }
            }
            if (keyboard.IsKeyDown(Keys.G))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.G))))
                {
                    input = "G";
                }
            }
            if (keyboard.IsKeyDown(Keys.H))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.H))))
                {
                    input = "H";
                }
            }
            if (keyboard.IsKeyDown(Keys.I))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.I))))
                {
                    input = "I";
                }
            }
            if (keyboard.IsKeyDown(Keys.J))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.J))))
                {
                    input = "J";
                }
            }
            if (keyboard.IsKeyDown(Keys.K))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.K))))
                {
                    input = "K";
                }
            }
            if (keyboard.IsKeyDown(Keys.L))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.L))))
                {
                    input = "L";
                }
            }
            if (keyboard.IsKeyDown(Keys.M))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.M))))
                {
                    input = "M";
                }
            }
            if (keyboard.IsKeyDown(Keys.N))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.N))))
                {
                    input = "N";
                }
            }
            if (keyboard.IsKeyDown(Keys.O))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.O))))
                {
                    input = "O";
                }
            }
            if (keyboard.IsKeyDown(Keys.P))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.P))))
                {
                    input = "P";
                }
            }
            if (keyboard.IsKeyDown(Keys.Q))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.Q))))
                {
                    input = "Q";
                }
            }            if (keyboard.IsKeyDown(Keys.R))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.R))))
                {
                    input = "R";
                }
            }
            if (keyboard.IsKeyDown(Keys.S))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.S))))
                {
                    input = "S";
                }
            }
            if (keyboard.IsKeyDown(Keys.T))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.T))))
                {
                    input = "T";
                }
            }
            if (keyboard.IsKeyDown(Keys.U))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.U))))
                {
                    input = "U";
                }
            }
            if (keyboard.IsKeyDown(Keys.V))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.V))))
                {
                    input = "V";
                }
            }
            if (keyboard.IsKeyDown(Keys.W))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.W))))
                {
                    input = "W";
                }
            }
            if (keyboard.IsKeyDown(Keys.X))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.X))))
                {
                    input = "X";
                }
            }
            if (keyboard.IsKeyDown(Keys.Y))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.Y))))
                {
                    input = "Y";
                }
            }            
            if (keyboard.IsKeyDown(Keys.Z))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.Z))))
                {
                    input = "Z";
                }
            }
            if (keyboard.IsKeyDown(Keys.D0))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D0))))
                {
                    input = "0";
                }
            }
            if (keyboard.IsKeyDown(Keys.D1))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D1))))
                {
                    input = "1";
                }
            }
            if (keyboard.IsKeyDown(Keys.D2))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D2))))
                {
                    input = "2";
                }
            }
            if (keyboard.IsKeyDown(Keys.D3))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D3))))
                {
                    input = "3";
                }
            }
            if (keyboard.IsKeyDown(Keys.D4))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D4))))
                {
                    input = "4";
                }
            }
            if (keyboard.IsKeyDown(Keys.D5))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D5))))
                {
                    input = "5";
                }
            }
            if (keyboard.IsKeyDown(Keys.D6))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D6))))
                {
                    input = "6";
                }
            }
            if (keyboard.IsKeyDown(Keys.D7))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D7))))
                {
                    input = "7";
                }
            }
            if (keyboard.IsKeyDown(Keys.D8))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D8))))
                {
                    input = "8";
                }
            } 
            if (keyboard.IsKeyDown(Keys.D9))//if key pressed up
            {
                if (!((preKeyboard.IsKeyDown(Keys.D9))))
                {
                    input = "9";
                }
            }


            output = myString + input;

            if ( keyboard.IsKeyDown(Keys.Back))
            {
                if (!((preKeyboard.IsKeyDown(Keys.Back))))
                {
                    output = "";
                }
            }

            return output;
        
        }
        #endregion


    }
}
