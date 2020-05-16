namespace NowPlaying.Core.InputSender
{
    public interface IInputSender
    {
        void SendSystemInput(ushort key);
        string GetSourceKey(ushort key);
    }
}
