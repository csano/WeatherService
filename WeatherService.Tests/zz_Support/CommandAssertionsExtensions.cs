using WpfApplication1;

namespace zz_Support
{
    public static class CommandAssertionsExtensions
    {
        public static CommandConstraint Should(this SimpleAsyncCommand actual)
        {
            return new CommandConstraint(actual);
        }
    }
}