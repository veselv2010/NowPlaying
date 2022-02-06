namespace NowPlaying.Core.InputSender
{
    public interface IKeyFormatter
    {
        string GetSourceKey(ushort keyCode);
        ushort GetVirtualKey(string sourceKey);
    }
}
