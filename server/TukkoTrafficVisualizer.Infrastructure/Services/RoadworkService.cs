using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Repositories.Cache;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class RoadworkService : IRoadworkService
{
    private readonly HttpClient _httpClient;
    private readonly IRoadworkCacheRepository _roadworkCacheRepository;

    public RoadworkService(HttpClient httpClient, IRoadworkCacheRepository roadworkCacheRepository)
    {
        _roadworkCacheRepository = roadworkCacheRepository;
        // creating new http client because injected client is not working
        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
    }

    public async Task<RoadworkContract> FetchRoadworkAsync()
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/traffic-message/v1/messages?situationType=ROAD_WORK&includeAreaGeometry=false");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException($"Failed to fetch roadworks: {responseMessage.ReasonPhrase}");
        }

        RoadworkContract? roadworkContract = await responseMessage.Content.ReadFromJsonAsync<RoadworkContract>();

        if (roadworkContract == null)
        {
            throw new InternalServerErrorException("Failed to fetch roadworks");
        }

        return roadworkContract;
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
                        Roadwork roadwork = MapRoadworkPhaseToRoadwork(phase);

                        await _roadworkCacheRepository.SetAsync(roadwork,expireSpan);
                    }
                }
            }
        }
    }

    public async Task<IEnumerable<Roadwork>> GetAsync(
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

    private Roadwork MapRoadworkPhaseToRoadwork(RoadWorkPhase phase)
    {
        RoadPoint? primaryPoint = phase.LocationDetails.RoadAddressLocation.PrimaryPoint;
        RoadPoint? secondaryPoint = phase.LocationDetails.RoadAddressLocation.SecondaryPoint;

        Roadwork roadwork = new Data.Entities.Roadwork
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
            WorkingHours = phase.WorkingHours.Select(wh => new Data.Entities.WorkingHours
            {
                Weekday = wh.Weekday,
                StartTime = wh.StartTime,
                EndTime = wh.EndTime,
            }).ToList(),
            Restrictions = phase.Restrictions.Select(r => new Data.Entities.Restriction
            {
                Name = r.RestrictionData.Name,
                Type = r.Type,
                Quantity = r.RestrictionData.Quantity,
                Unit = r.RestrictionData.Unit,
            }).ToList()
        };

        return roadwork;
    }
}