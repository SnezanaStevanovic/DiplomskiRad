using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Timesheet.Common
{
    public class TransactionScopeCreator
    {
        public static TransactionScope Create()
        {
            TransactionOptions transactionOptions = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            return transactionScope;
        }
    }
}
