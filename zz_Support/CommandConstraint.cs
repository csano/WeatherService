using System;
using FluentAssertions;
using FluentAssertions.Execution;
using JetBrains.Annotations;

namespace MvvmCore.Tests.zz_Support
{
    public class CommandConstraint
    {
        public CommandConstraint([CanBeNull] SimpleCommand actual)
        {
            Actual = actual;
        }

        [CanBeNull]
        private SimpleCommand Actual { get; set; }

        [StringFormatMethod("reason"), NotNull]
        public AndConstraint<CommandConstraint> Call([CanBeNull] Action expected,
            string reason = null,
            params object[] reasonArgs)
        {
            var actualOperation = Actual.IfValid(a => a.Operation);
            Execute.Assertion.ForCondition(expected == actualOperation)
                .BecauseOf(reason, reasonArgs)
                .FailWith("Command is bound to call the wrong method. Expected:\r\n{0}, but will call\r\n{1} instead.",
                    expected,
                    actualOperation);
            return new AndConstraint<CommandConstraint>(this);
        }

        [StringFormatMethod("reason"), NotNull]
        public AndConstraint<CommandConstraint> BeEnabledWhen([CanBeNull] Func<bool> expected,
            string reason = null,
            params object[] reasonArgs)
        {
            var actualIsEnabled = Actual.IfValid(a => a.IsEnabled);
            Execute.Assertion.ForCondition(expected == actualIsEnabled)
                .BecauseOf(reason, reasonArgs)
                .FailWith(
                    "Command is bound use the wrong method to determine when it is enabled. Expected:\r\n{0}, but will call\r\n{1} instead.",
                    expected,
                    actualIsEnabled);
            return new AndConstraint<CommandConstraint>(this);
        }
    }
}