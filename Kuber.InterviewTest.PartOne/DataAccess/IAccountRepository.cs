using System;

namespace Kuber.InterviewTest.PartOne.DataAccess
{
    public interface IAccountRepository
    {
        Account GetAccountById(Guid accountId);

        void Update(Account account);
    }
}
