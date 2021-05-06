using System;

namespace Kuber.InterviewTest.PartOne
{
    public class Account
    {
        public const decimal PayInLimit = 4000m; // make this property driven instead of hard coded value

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        // PV : Refactored code : added below 2 methods.
        private Domain.Services.INotificationService notificationService;
        public Account(Domain.Services.INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public bool checkEligibilityForWithdrawOrTransfer (decimal Balance, decimal amount, string UserEmail) {
            //Validations
            //Balance & Amount cannot be Negative
            //Email is in valid format - Any how it is coming from repository

            //Log Error
            //Catch Exception
            var fromBalance = Balance - amount;
            if(fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer/withdraw");
            }

            if (fromBalance < 500m)
            {
                this.notificationService.NotifyFundsLow(UserEmail);
            }
			
			return true;
			
		}
		
		public bool checkEligibilityForAcceptance (decimal paidInBalance, decimal amount, string UserEmail) {
			var paidIn = paidInBalance + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(UserEmail);
            }
			
			return true;
		}
    }
}
