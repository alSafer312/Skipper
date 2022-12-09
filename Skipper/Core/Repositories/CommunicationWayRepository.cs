namespace Skipper.Core.Repositories
{
    public class CommunicationWayRepository : GenericRepository<CommunicationWay>, ICommunicationWayRepository
    {
        public CommunicationWayRepository(DataContext context) : base(context)
        {
        }
    }
}
