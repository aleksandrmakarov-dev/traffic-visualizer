using TukkoTrafficVisualizer.Cache.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class RoadworkCacheService : IRoadworkCacheService
{
    private readonly IRoadworkCacheRepository _roadworkCacheRepository;

    public RoadworkCacheService(IRoadworkCacheRepository roadworkCacheRepository)
    {
        _roadworkCacheRepository = roadworkCacheRepository;
    }

    public async Task SaveRoadworksAsync(RoadworkContract roadworkContract)
    {
        foreach (Feature feature in roadworkContract.Features)
        {
            foreach (Announcement announcement in feature.Properties.Announcements)
            {
                foreach (RoadWorkPhase phase in announcement.RoadWorkPhases)
                {
                    // check if roadwork is expired
                    TimeSpan expireSpan = phase.TimeAndDuration.EndTime - DateTime.UtcNow;

                    if (expireSpan > TimeSpan.Zero)
                    {
                        Cache.Entities.Roadwork roadwork = MapRoadworkPhaseToRoadwork(phase);

                        await _roadworkCacheRepository.SetAsync(roadwork, expireSpan);
                    }
                }
            }
        }
    }

    public async Task<IEnumerable<Cache.Entities.Roadwork>> GetAsync(
        int primaryPointRoadNumber,
        int primaryPointRoadSection,
        int secondaryPointRoadNumber,
        int secondaryPointRoadSection,
        string severity
        )
    {
        // IEnumerable<Data.Redis.Entities.Roadwork> roadworkList = await _roadworkCacheRepository.FilterAsync(
        //    primaryPointRoadNumber, primaryPointRoadSection, secondaryPointRoadNumber, secondaryPointRoadSection,
        //    startTimeOnAfter, startTimeOnBefore, severity);


        return await _roadworkCacheRepository.GetAsync(severity
            );
    }

    private Cache.Entities.Roadwork MapRoadworkPhaseToRoadwork(RoadWorkPhase phase)
    {
        RoadPoint? primaryPoint = phase.LocationDetails.RoadAddressLocation.PrimaryPoint;
        RoadPoint? secondaryPoint = phase.LocationDetails.RoadAddressLocation.SecondaryPoint;

        Cache.Entities.Roadwork roadwork = new Cache.Entities.Roadwork
        {
            Id = phase.Id.Substring(4),
            PrimaryPointRoadNumber = primaryPoint?.RoadAddress.Road,
            PrimaryPointRoadSection = primaryPoint?.RoadAddress.RoadSection,
            SecondaryPointRoadNumber = secondaryPoint?.RoadAddress.Road,
            SecondaryPointRoadSection = secondaryPoint?.RoadAddress.RoadSection,
            Severity = phase.Severity,
            StartTime = phase.TimeAndDuration.StartTime,
            EndTime = phase.TimeAndDuration.EndTime,
            Direction = phase.LocationDetails.RoadAddressLocation.Direction,
            WorkingHours = phase.WorkingHours.Select(wh => new Cache.Entities.WorkingHours
            {
                Weekday = wh.Weekday,
                StartTime = wh.StartTime,
                EndTime = wh.EndTime,
            }),
            WorkTypes = phase.WorkTypes.Select(wt => new Cache.Entities.WorkType
            {
                Type = wt.Type,
                Description = wt.Description
            }),
            Restrictions = phase.Restrictions.Select(r => new Cache.Entities.Restriction
            {
                Name = r.RestrictionData.Name,
                Type = r.Type,
                Quantity = r.RestrictionData.Quantity,
                Unit = r.RestrictionData.Unit,
            })
        };

        return roadwork;
    }
}