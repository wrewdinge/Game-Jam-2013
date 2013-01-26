//Author: Stephen Lane
//Date: 10/10/2012
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProject
{

    public enum MouseButtons
    {
        LeftButton,
        MiddleButton,
        RightButton
    }

    public enum OtherInput
    {
        LeftStickFlickUp,
        LeftStickFlickDown,
        LeftStickFlickLeft,
        LeftStickFlickRight,
        RightStickFlickUp,
        RightStickFlickDown,
        RightStickFlickLeft,
        RightStickFlickRight
    }

    public struct InputState
    {
        public bool Down;
        public bool Pressed;
        public bool Released;
    }

    public enum OtherInputState
    {
        Pressed,
        Released
    }

    public class OtherState
    {

        public OtherInputState
            LeftStickFlickUp,
            LeftStickFlickDown,
            LeftStickFlickLeft,
            LeftStickFlickRight,
            RightStickFlickUp,
            RightStickFlickDown,
            RightStickFlickLeft,
            RightStickFlickRight;

        public static OtherState GetState()
        {

            GamePadState state = GamePad.GetState(PlayerIndex.One);

            OtherState newState = new OtherState();

            newState.LeftStickFlickDown = (state.ThumbSticks.Left.Y > 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.LeftStickFlickUp = (state.ThumbSticks.Left.Y < 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.LeftStickFlickLeft = (state.ThumbSticks.Left.X < 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.LeftStickFlickRight = (state.ThumbSticks.Left.X > 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.RightStickFlickDown = (state.ThumbSticks.Right.Y > 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.RightStickFlickUp = (state.ThumbSticks.Right.Y < 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.RightStickFlickLeft = (state.ThumbSticks.Right.X < 0.0f ? OtherInputState.Pressed : OtherInputState.Released);
            newState.RightStickFlickRight = (state.ThumbSticks.Right.X > 0.0f ? OtherInputState.Pressed : OtherInputState.Released);

            return newState;

        }

    }

    public class InputManager
    {

        private static KeyboardState
            sKeyState;

        private static GamePadState
            sGamePadState;

        private static MouseState
            sMouseState;

        private static OtherState
            sOtherState;

        private static Dictionary<string, List<Keys>>
            sKeyAlias;

        private static Dictionary<string, List<Buttons>>
            sGamePadAlias;

        private static Dictionary<string, List<MouseButtons>>
            sMouseAlias;

        private static Dictionary<string, List<OtherInput>>
            sOtherAlias;

        private static Dictionary<Keys, InputState>
            sKeyInputState;

        private static Dictionary<Buttons, InputState>
            sGamePadInputState;

        private static Dictionary<MouseButtons, InputState>
            sMouseInputState;

        private static Dictionary<OtherInput, InputState>
            sOtherInputState;

        public InputManager()
        {

            sKeyState = Keyboard.GetState();
            sGamePadState = GamePad.GetState(PlayerIndex.One);
            sMouseState = Mouse.GetState();
            sOtherState = OtherState.GetState();

            sKeyAlias = new Dictionary<string, List<Keys>>();
            sGamePadAlias = new Dictionary<string, List<Buttons>>();
            sMouseAlias = new Dictionary<string, List<MouseButtons>>();
            sOtherAlias = new Dictionary<string, List<OtherInput>>();

            sKeyInputState = new Dictionary<Keys, InputState>();
            sGamePadInputState = new Dictionary<Buttons, InputState>();
            sMouseInputState = new Dictionary<MouseButtons, InputState>();
            sOtherInputState = new Dictionary<OtherInput, InputState>();

        }

        public void Alias(string pIndex, Keys pKey)
        {

            if (!sKeyAlias.ContainsKey(pIndex))
                sKeyAlias.Add(pIndex, new List<Keys>());

            sKeyAlias[pIndex].Add(pKey);

            if (!sKeyInputState.ContainsKey(pKey))
            {

                InputState state = new InputState();

                state.Down = sKeyState.IsKeyDown(pKey);
                state.Pressed = false;
                state.Released = false;

                sKeyInputState.Add(pKey, state);
            }

        }

        public void Alias(string pIndex, Buttons pButton)
        {

            if (!sGamePadAlias.ContainsKey(pIndex))
                sGamePadAlias.Add(pIndex, new List<Buttons>());

            sGamePadAlias[pIndex].Add(pButton);

            if (!sGamePadInputState.ContainsKey(pButton))
            {

                InputState state = new InputState();

                state.Down = sGamePadState.IsButtonDown(pButton);
                state.Pressed = false;
                state.Released = false;

                sGamePadInputState.Add(pButton, state);
            }

        }

        public void Alias(string pIndex, MouseButtons pMouseButton)
        {

            if (!sMouseAlias.ContainsKey(pIndex))
                sMouseAlias.Add(pIndex, new List<MouseButtons>());

            sMouseAlias[pIndex].Add(pMouseButton);

            if (!sMouseInputState.ContainsKey(pMouseButton))
            {

                InputState state = new InputState();

                ButtonState mouseState = GetMouseButtonState(pMouseButton, sMouseState);

                state.Down = (mouseState == ButtonState.Pressed);
                state.Pressed = false;
                state.Released = false;

                sMouseInputState.Add(pMouseButton, state);
            }

        }

        public void Alias(string pIndex, OtherInput pOtherInput)
        {

            if (!sOtherAlias.ContainsKey(pIndex))
                sOtherAlias.Add(pIndex, new List<OtherInput>());

            sOtherAlias[pIndex].Add(pOtherInput);

            if (!sOtherInputState.ContainsKey(pOtherInput))
            {

                InputState state = new InputState();

                OtherInputState otherState = GetOtherState(pOtherInput, sOtherState);

                state.Down = (otherState == OtherInputState.Pressed);
                state.Pressed = false;
                state.Released = false;

                sOtherInputState.Add(pOtherInput, state);
            }

        }

        public void Listen(Keys pKey)
        {

            if (!sKeyInputState.ContainsKey(pKey))
            {

                InputState state = new InputState();

                state.Down = sKeyState.IsKeyDown(pKey);
                state.Pressed = false;
                state.Released = false;

                sKeyInputState.Add(pKey, state);
            }

        }

        public void Listen(Buttons pButton)
        {

            if (!sGamePadInputState.ContainsKey(pButton))
            {

                InputState state = new InputState();

                state.Down = sGamePadState.IsButtonDown(pButton);
                state.Pressed = false;
                state.Released = false;

                sGamePadInputState.Add(pButton, state);
            }

        }

        public void Listen(MouseButtons pMouseButton)
        {

            if (!sMouseInputState.ContainsKey(pMouseButton))
            {

                InputState state = new InputState();

                ButtonState mouseState = GetMouseButtonState(pMouseButton, sMouseState);

                state.Down = (mouseState == ButtonState.Pressed);
                state.Pressed = false;
                state.Released = false;

                sMouseInputState.Add(pMouseButton, state);
            }

        }

        public void Listen(OtherInput pOtherInput)
        {

            if (!sOtherInputState.ContainsKey(pOtherInput))
            {

                InputState state = new InputState();

                OtherInputState otherState = GetOtherState(pOtherInput, sOtherState);

                state.Down = (otherState == OtherInputState.Pressed);
                state.Pressed = false;
                state.Released = false;

                sOtherInputState.Add(pOtherInput, state);
            }

        }

        public static bool Up(string alias)
        {

            bool ret;

            if (GetKeyState(alias).Down || GetGamePadState(alias).Down || GetMouseState(alias).Down || GetOtherState(alias).Down)
                ret = false;
            else
                ret = true;

            return ret;

        }

        public static bool Down(string alias)
        {

            bool ret;

            if (GetKeyState(alias).Down || GetGamePadState(alias).Down || GetMouseState(alias).Down || GetOtherState(alias).Down)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Pressed(string alias)
        {

            bool ret;

            if (GetKeyState(alias).Pressed || GetGamePadState(alias).Pressed || GetMouseState(alias).Pressed || GetOtherState(alias).Pressed)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Released(string alias)
        {

            bool ret;

            if (GetKeyState(alias).Released || GetGamePadState(alias).Released || GetMouseState(alias).Released || GetOtherState(alias).Released)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Up(Keys pKey)
        {

            bool ret;

            if (GetKeyState(pKey).Down)
                ret = false;
            else
                ret = true;

            return ret;

        }

        public static bool Up(Buttons pButton)
        {

            bool ret;

            if (GetGamePadState(pButton).Down)
                ret = false;
            else
                ret = true;

            return ret;

        }

        public static bool Up(MouseButtons pMouseButton)
        {

            bool ret;

            if (GetMouseState(pMouseButton).Down)
                ret = false;
            else
                ret = true;

            return ret;

        }

        public static bool Up(OtherInput pOtherInput)
        {

            bool ret;

            if (GetOtherState(pOtherInput).Down)
                ret = false;
            else
                ret = true;

            return ret;

        }

        public static bool Down(Keys pKey)
        {

            bool ret;

            if (GetKeyState(pKey).Down)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Down(Buttons pButton)
        {

            bool ret;

            if (GetGamePadState(pButton).Down)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Down(MouseButtons pMouseButton)
        {

            bool ret;

            if (GetMouseState(pMouseButton).Down)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Down(OtherInput pOtherInput)
        {

            bool ret;

            if (GetOtherState(pOtherInput).Down)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Pressed(Keys pKey)
        {

            bool ret;

            if (GetKeyState(pKey).Pressed)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Pressed(Buttons pButton)
        {

            bool ret;

            if (GetGamePadState(pButton).Pressed)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Pressed(MouseButtons pMouseButton)
        {

            bool ret;

            if (GetMouseState(pMouseButton).Pressed)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Pressed(OtherInput pOtherInput)
        {

            bool ret;

            if (GetOtherState(pOtherInput).Pressed)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Released(Keys pKey)
        {

            bool ret;

            if (GetKeyState(pKey).Released)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Released(Buttons pButtons)
        {

            bool ret;

            if (GetGamePadState(pButtons).Released)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Released(MouseButtons pMouseButton)
        {

            bool ret;

            if (GetMouseState(pMouseButton).Released)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static bool Released(OtherInput pOtherInput)
        {

            bool ret;

            if (GetOtherState(pOtherInput).Released)
                ret = true;
            else
                ret = false;

            return ret;

        }

        public static Vector2 MousePos()
        {

            return new Vector2(sMouseState.X, sMouseState.Y);

        }

        private static InputState GetKeyState(string alias)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sKeyAlias.ContainsKey(alias))
            {
                foreach (Keys key in sKeyAlias[alias])
                {
                    if (sKeyInputState.ContainsKey(key))
                    {
                        state.Down |= sKeyInputState[key].Down;
                        state.Pressed |= sKeyInputState[key].Pressed;
                        state.Released |= sKeyInputState[key].Released;
                    }
                }
            }

            return state;

        }

        private static InputState GetKeyState(Keys pKey)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sKeyInputState.ContainsKey(pKey))
            {
                state.Down |= sKeyInputState[pKey].Down;
                state.Pressed |= sKeyInputState[pKey].Pressed;
                state.Released |= sKeyInputState[pKey].Released;
            }

            return state;

        }

        private static InputState GetGamePadState(string alias)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sGamePadAlias.ContainsKey(alias))
            {
                foreach (Buttons button in sGamePadAlias[alias])
                {
                    if (sGamePadInputState.ContainsKey(button))
                    {
                        state.Down |= sGamePadInputState[button].Down;
                        state.Pressed |= sGamePadInputState[button].Pressed;
                        state.Released |= sGamePadInputState[button].Released;
                    }
                }
            }

            return state;

        }

        private static InputState GetGamePadState(Buttons pButton)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sGamePadInputState.ContainsKey(pButton))
            {
                state.Down |= sGamePadInputState[pButton].Down;
                state.Pressed |= sGamePadInputState[pButton].Pressed;
                state.Released |= sGamePadInputState[pButton].Released;
            }

            return state;

        }

        private static InputState GetMouseState(string alias)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sMouseAlias.ContainsKey(alias))
            {
                foreach (MouseButtons mouseButton in sMouseAlias[alias])
                {
                    if (sMouseInputState.ContainsKey(mouseButton))
                    {
                        state.Down |= sMouseInputState[mouseButton].Down;
                        state.Pressed |= sMouseInputState[mouseButton].Pressed;
                        state.Released |= sMouseInputState[mouseButton].Released;
                    }
                }
            }

            return state;

        }

        private static InputState GetMouseState(MouseButtons pMouseButton)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sMouseInputState.ContainsKey(pMouseButton))
            {
                state.Down |= sMouseInputState[pMouseButton].Down;
                state.Pressed |= sMouseInputState[pMouseButton].Pressed;
                state.Released |= sMouseInputState[pMouseButton].Released;
            }

            return state;

        }

        private static InputState GetOtherState(string alias)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sOtherAlias.ContainsKey(alias))
            {
                foreach (OtherInput otherInput in sOtherAlias[alias])
                {
                    if (sOtherInputState.ContainsKey(otherInput))
                    {
                        state.Down |= sOtherInputState[otherInput].Down;
                        state.Pressed |= sOtherInputState[otherInput].Pressed;
                        state.Released |= sOtherInputState[otherInput].Released;
                    }
                }
            }

            return state;

        }

        private static InputState GetOtherState(OtherInput pOtherInput)
        {

            InputState state = new InputState();

            state.Down = false;
            state.Pressed = false;
            state.Released = false;

            if (sOtherInputState.ContainsKey(pOtherInput))
            {
                state.Down |= sOtherInputState[pOtherInput].Down;
                state.Pressed |= sOtherInputState[pOtherInput].Pressed;
                state.Released |= sOtherInputState[pOtherInput].Released;
            }

            return state;

        }

        private ButtonState GetMouseButtonState(MouseButtons pMouseButton, MouseState pMouseState)
        {

            ButtonState state = ButtonState.Released;

            switch (pMouseButton)
            {
                case MouseButtons.LeftButton:

                    state = pMouseState.LeftButton;

                    break;
                case MouseButtons.MiddleButton:

                    state = pMouseState.MiddleButton;

                    break;
                case MouseButtons.RightButton:

                    state = pMouseState.RightButton;

                    break;
            }

            return state;

        }

        private OtherInputState GetOtherState(OtherInput pOtherInput, OtherState pOtherState)
        {

            OtherInputState state = OtherInputState.Released;

            switch (pOtherInput)
            {
                case OtherInput.LeftStickFlickDown:

                    state = pOtherState.LeftStickFlickDown;

                    break;
                case OtherInput.LeftStickFlickLeft:

                    state = pOtherState.LeftStickFlickLeft;

                    break;
                case OtherInput.LeftStickFlickRight:

                    state = pOtherState.LeftStickFlickRight;

                    break;
                case OtherInput.LeftStickFlickUp:

                    state = pOtherState.LeftStickFlickUp;

                    break;
                case OtherInput.RightStickFlickDown:

                    state = pOtherState.RightStickFlickDown;

                    break;
                case OtherInput.RightStickFlickLeft:

                    state = pOtherState.RightStickFlickLeft;

                    break;
                case OtherInput.RightStickFlickRight:

                    state = pOtherState.RightStickFlickRight;

                    break;
                case OtherInput.RightStickFlickUp:

                    state = pOtherState.RightStickFlickUp;

                    break;
            }

            return state;

        }

        public void Update(GameTime pGameTime)
        {

            UpdateKeyboard();
            UpdateGamePad();
            UpdateMouse();
            UpdateOther();

        }

        private void UpdateKeyboard()
        {

            KeyboardState oldState = sKeyState;
            sKeyState = Keyboard.GetState();

            Keys[] keys = sKeyInputState.Keys.ToArray<Keys>();

            foreach (Keys key in keys)
            {

                InputState state = new InputState();

                state.Down = sKeyState.IsKeyDown(key);
                state.Pressed = (sKeyState.IsKeyDown(key) && oldState.IsKeyUp(key));
                state.Released = (sKeyState.IsKeyUp(key) && oldState.IsKeyDown(key));

                sKeyInputState[key] = state;

            }

        }

        private void UpdateGamePad()
        {

            GamePadState oldState = sGamePadState;
            sGamePadState = GamePad.GetState(PlayerIndex.One);

            Buttons[] buttons = sGamePadInputState.Keys.ToArray<Buttons>();

            foreach (Buttons button in buttons)
            {

                InputState state = new InputState();

                state.Down = sGamePadState.IsButtonDown(button);
                state.Pressed = (sGamePadState.IsButtonDown(button) && oldState.IsButtonUp(button));
                state.Released = (sGamePadState.IsButtonUp(button) && oldState.IsButtonDown(button));

                sGamePadInputState[button] = state;

            }

        }

        private void UpdateMouse()
        {

            MouseState oldState = sMouseState;
            sMouseState = Mouse.GetState();

            MouseButtons[] mouseButtons = sMouseInputState.Keys.ToArray<MouseButtons>();

            foreach (MouseButtons mouseButton in mouseButtons)
            {

                InputState state = new InputState();

                ButtonState curMouseState = GetMouseButtonState(mouseButton, sMouseState);
                ButtonState oldMouseState = GetMouseButtonState(mouseButton, oldState);

                state.Down = (curMouseState == ButtonState.Pressed);
                state.Pressed = ((curMouseState == ButtonState.Pressed) && (oldMouseState == ButtonState.Released));
                state.Released = ((curMouseState == ButtonState.Released) && (oldMouseState == ButtonState.Pressed));

                sMouseInputState[mouseButton] = state;

            }

        }

        private void UpdateOther()
        {

            OtherState oldState = sOtherState;
            sOtherState = OtherState.GetState();

            OtherInput[] otherInputs = sOtherInputState.Keys.ToArray<OtherInput>();

            foreach (OtherInput otherInput in otherInputs)
            {

                InputState state = new InputState();

                OtherInputState curOtherState = GetOtherState(otherInput, sOtherState);
                OtherInputState oldOtherState = GetOtherState(otherInput, oldState);

                state.Down = (curOtherState == OtherInputState.Pressed);
                state.Pressed = ((curOtherState == OtherInputState.Pressed) && (oldOtherState == OtherInputState.Released));
                state.Released = ((curOtherState == OtherInputState.Released) && (oldOtherState == OtherInputState.Pressed));

                sOtherInputState[otherInput] = state;

            }

        }

    }

}
