namespace GothicOnline.G2.DotNet.Squirrel
{
    /// <summary>
    /// Defines the execution states of the squirrel vm.
    /// </summary>
    public enum VmState
    {
        /// <summary>
        /// The vm is idle.
        /// </summary>
        SqVmstateIdle = 0,
        /// <summary>
        /// The vm is running.
        /// </summary>
        SqVmstateRunning = 1,
        /// <summary>
        /// The vm is suspended.
        /// </summary>
        SqVmstateSuspended = 2,
    }
}
