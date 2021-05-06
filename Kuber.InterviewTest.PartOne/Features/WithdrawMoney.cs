using Kuber.InterviewTest.PartOne.DataAccess;
using Kuber.InterviewTest.PartOne.Domain.Services;
using System;

namespace Kuber.InterviewTest.PartOne.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        //public WithdrawMoney(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            
            if(from.checkEligibilityForWithdrawOrTransfer(from.Balance, amount, from.User.Email))
			{ 
                    this.notificationService.NotifyTxnSuccess(from.User.Email);
            }

            from.Balance = from.Balance - amount;
            from.Withdrawn = from.Withdrawn - amount;

            this.accountRepository.Update(from);
        }
    }
}