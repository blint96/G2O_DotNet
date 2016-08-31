namespace G2O_Framework
{
    using GothicOnline.G2.DotNet.Squirrel;

    public interface IPlugin
    {
        void Init(IServer server, IPluginManager pluginManager, ISquirrelApi squirrelApi);
    }
}
