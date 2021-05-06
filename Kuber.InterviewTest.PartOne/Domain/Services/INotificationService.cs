namespace Kuber.InterviewTest.PartOne.Domain.Services
{
    public interface INotificationService
    {
        void NotifyApproachingPayInLimit(string emailAddress);

        void NotifyFundsLow(string emailAddress);
		
		// New method for success notification.
        void NotifyTxnSuccess(string emailAddress);
    }
}
