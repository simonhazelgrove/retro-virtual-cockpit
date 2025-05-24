using System.Collections.Generic;
using SharpDX.DirectInput;

namespace RetroVirtualCockpit.Server.Receivers.Joystick
{
    public class ButtonValueChangedEvent : BaseJoystickEvent, IJoystickEvent
    {
        public int ButtonIndex { get; set; }

        public bool? Value { get; set; }

        public List<string> GameActions;

        private string _lastGameAction;

        public ButtonValueChangedEvent()
        {
        }

        public ButtonValueChangedEvent(int buttonIndex, string gameAction) : base(gameAction)
        {
            ButtonIndex = buttonIndex;
        }

        public ButtonValueChangedEvent(int buttonIndex, bool value, string gameAction) : base(gameAction)
        {
            ButtonIndex = buttonIndex;
            Value = value;
        }

        public ButtonValueChangedEvent(int buttonIndex, bool value, List<string> gameActions)
        {
            ButtonIndex = buttonIndex;
            Value = value;
            GameActions = gameActions;
        }

        public override bool Evaluate(JoystickState previousState, JoystickState currentState)
        {
            return (!Value.HasValue && currentState.Buttons[ButtonIndex] != previousState.Buttons[ButtonIndex])
                || (Value.HasValue && currentState.Buttons[ButtonIndex] == Value && previousState.Buttons[ButtonIndex] != Value);
        }

        public override string GameAction
        {
            get
            {
                if (GameActions != null)
                {
                    // Cycle through range of actions
                    return NextGameAction();
                }

                return base.GameAction;
            }
            set
            {
                base.GameAction = value;
            }
        }

        private string NextGameAction()
        {
            var actionIndex = GameActions.IndexOf(_lastGameAction) + 1;

            if (actionIndex == GameActions.Count)
            {
                actionIndex = 0;
            }

            var gameAction = GameActions[actionIndex];

            _lastGameAction = gameAction;
            
            return gameAction;
        }
    }
}
