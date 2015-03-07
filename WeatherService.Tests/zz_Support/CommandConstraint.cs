using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using WpfApplication1;

namespace zz_Support
{
    public class CommandConstraint
    {
        public CommandConstraint(SimpleAsyncCommand actual)
        {
            Actual = actual;
        }

        private SimpleAsyncCommand Actual { get; set; }

        public AndConstraint<CommandConstraint> Call(Func<Task> expected,
            string reason = null,
            params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(expected == Actual.AsyncOperation)
                .BecauseOf(reason, reasonArgs)
                .FailWith("Command is bound to call the wrong method. Expected:\r\n{0}, but will call\r\n{1} instead.",
                    expected,
                    Actual.AsyncOperation);
            return new AndConstraint<CommandConstraint>(this);
        }
    }
}