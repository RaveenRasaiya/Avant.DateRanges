using Avant.Application.Interfaces;
using Avant.Domain.Enums;

namespace Avant.Application
{
    public class HolidayProviderService : IHolidayProviderService
    {
        public IHolidayService GetService(HolidayType holidayType)
        {
            switch (holidayType)
            {
                case HolidayType.Fixed:
                    return new FixedHolidayService();
                case HolidayType.FollowingWeekDay:
                    return new FollowingWeekHolidayService();
                case HolidayType.AlwaysSameDay:
                    return new SameDayHolidayService();
                default:
                    return null;
            }
        }
    }
}
