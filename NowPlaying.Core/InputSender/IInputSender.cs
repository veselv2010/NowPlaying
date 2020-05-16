namespace NowPlaying.Core.InputSender
{
    interface IInputSender
    {
        void SendSystemInput(ushort key);
        string GetSourceKey(ushort key);
    }
}
