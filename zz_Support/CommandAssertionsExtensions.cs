namespace MvvmCore.Tests.zz_Support
{
    public static class CommandAssertionsExtensions
    {
        public static CommandConstraint Should(this SimpleCommand actual)
        {
            return new CommandConstraint(actual);
        }
    }
}