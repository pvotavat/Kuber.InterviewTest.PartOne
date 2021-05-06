using Kuber.InterviewTest.PartOne.DataAccess;
using Kuber.InterviewTest.PartOne.Domain.Services;
using System;

namespace Kuber.InterviewTest.PartOne.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

			// PV : Start Refactor code.

			// Moved the code for transfer eligibility check to the Account domain class
            /*var fromBalance = from.Balance - amount; 
			
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }*/
			
			if(from.checkEligibilityForWithdrawOrTransfer(from.Balance, amount, from.User.Email))
			{ this.notificationService.NotifyTxnSuccess(from.User.Email); }
			
			// Moved the code for acceptance eligibility check to the Account domain class
            /*var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }*/
			
			
			if(to.checkEligibilityForAcceptance(to.PaidIn, amount, to.User.Email))
			{ this.notificationService.NotifyTxnSuccess(to.User.Email); }
			
			//End Refactor code
			
            from.Balance = from.Balance - amount;
            from.Withdrawn = from.Withdrawn - amount;

            to.Balance = to.Balance + amount;
            to.PaidIn = to.PaidIn + amount;

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }

        //private bool checkEligibilityForAcceptance(decimal paidIn, decimal amount, string email)
        //{
        //    throw new NotImplementedException();
        //}

        //private bool checkEligibilityForWithdrawOrTransfer(decimal balance, decimal amount, string email)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
