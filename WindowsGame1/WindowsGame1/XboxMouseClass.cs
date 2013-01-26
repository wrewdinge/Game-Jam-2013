using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsGame1
{
    public class XboxMouseClass
    {   
        private Vector2 mMousePos;
        //private ControlsClass Controls;

        public XboxMouseClass()
        {
            mMousePos = new Vector2(40, 40);
            //Controls = new ControlsClass();
        }
        
#if XBOX
        public void updateMousePos()
        {
            if (Controls.right())
                mMousePos.X += 2;
            if (Controls.left())
                mMousePos.X -= 2;
            if (Controls.up())
                mMousePos.Y -= 2;
            if (Controls.down())
                mMousePos.Y += 2;
        }

        public bool click()
        {
            if (Controls.trigger())
                return true;
            else
                return false;
        }
        
#endif

#if WINDOWS
        public void updateMousePos()
        {
            mMousePos = new Vector2( Mouse.GetState().X, Mouse.GetState().Y );
        }

        public bool click()
        {
            
            //if (Controls.mouseClick())
              //  return true;
            //else
              return false;
            
        }

#endif 

        public Vector2 getMousePos()
        {
            return mMousePos;
        }

        public float getMouseX()
        {
            return mMousePos.X;
        }

        public float getMouseY()
        {
            return mMousePos.Y;
        }

    }
        
}

