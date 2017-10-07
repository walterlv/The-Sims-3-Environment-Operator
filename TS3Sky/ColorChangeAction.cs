using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TS3Sky
{
    public class ColorChangeAction
    {
        public ColorPickBar ActionBar;
        public int PickIndex;
        public Color OldColor;
        public Color NewColor;

        public static Stack<ColorChangeAction> Actions = new Stack<ColorChangeAction>();
        public static Stack<ColorChangeAction> RedoActions = new Stack<ColorChangeAction>();

        private ColorChangeAction(ColorPickBar actionBar, int pickIndex, Color oldColor, Color newColor)
        {
            this.ActionBar = actionBar;
            this.PickIndex = pickIndex;
            this.OldColor = Color.FromArgb(oldColor.A, oldColor.R, oldColor.G, oldColor.B);
            this.NewColor = Color.FromArgb(newColor.A, newColor.R, newColor.G, newColor.B);
        }

        public static void PerformAction(ColorPickBar actionBar, int pickIndex, Color oldColor, Color newColor)
        {
            Actions.Push(new ColorChangeAction(actionBar, pickIndex, oldColor, newColor));
            RedoActions.Clear();
        }
        public static ColorChangeAction UndoAction()
        {
            if (Actions.Count > 0)
            {
                ColorChangeAction action = Actions.Pop();
                RedoActions.Push(action);
                return action;
            }
            else return null;
        }
        public static ColorChangeAction RedoAction()
        {
            if (RedoActions.Count > 0)
            {
                ColorChangeAction action = RedoActions.Pop();
                Actions.Push(action);
                return action;
            }
            else return null;
        }
        public static ColorChangeAction GetUndoAction()
        {
            if (Actions.Count > 0) return Actions.Peek();
            else return null;
        }
        public static ColorChangeAction GetRedoAction()
        {
            if (RedoActions.Count > 0) return RedoActions.Peek();
            else return null;
        }
        public static void ClearActions()
        {
            Actions.Clear();
            RedoActions.Clear();
        }
    }
}
