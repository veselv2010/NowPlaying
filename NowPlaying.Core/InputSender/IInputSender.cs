namespace NowPlaying.Core.InputSender
{
    public interface IInputSender
    {
        void SendSystemInput(ushort keyCode);
    }
}
