namespace RetroVirtualCockpit.Client.Messages
{
    public class KeyboardMessage : Message
    {
        public KeyDirection Direction { get; set; }

        public int? DelayUntilKeyUp { get; set; }

        public KeyboardMessage()
        {
            Direction = KeyDirection.Down;
            DelayUntilKeyUp = 200;
        }
    }
}
