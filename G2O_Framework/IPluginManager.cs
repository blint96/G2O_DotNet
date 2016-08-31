namespace G2O_Framework
{
    public interface IPluginManager
    {
        /// <summary>
        /// Gets the directory of the calling plugin.
        /// </summary>
        /// <returns> The directory of the calling plugin.</returns>
        string GetLocalPluginDirectory();

        string GetPluginFilePath();

        /// <summary>
        /// Gets the public interface of another plugin.
        /// <remarks>Can return null if the given interface is not available.</remarks>
        /// </summary>
        /// <typeparam name="TPlugin">The type of the interface that should be returned.</typeparam>
        /// <returns>The instance of the interface that was provided by the other plugin</returns>
        TPlugin GetPluginInterface<TPlugin>();

        int PluginCount { get; }


    }
}
