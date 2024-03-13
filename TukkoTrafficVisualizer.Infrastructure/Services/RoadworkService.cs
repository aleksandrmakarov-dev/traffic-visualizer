﻿using System.Net;
using System.Net.Http.Json;
using Server.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Data.Redis.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class RoadworkService : IRoadworkService
{
    private readonly HttpClient _httpClient;

    public RoadworkService(HttpClient httpClient)
    {
        // creating new http client because injected client is not working
        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
    }

    public async Task<RoadworkContract?> FetchLatestRoadworkAsync()
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/traffic-message/v1/messages?situationType=ROAD_WORK&includeAreaGeometry=false");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException($"Failed to get locations: {responseMessage.ReasonPhrase}");
        }

        RoadworkContract? roadworkContract = await responseMessage.Content.ReadFromJsonAsync<RoadworkContract>();

        return roadworkContract;
    }

    public async Task SaveRoadworkAsync(RoadworkContract roadworkContract)
    {
        foreach (Feature feature in roadworkContract.Features)
        {
            foreach (Announcement announcement in feature.Properties.Announcements)
            {
                foreach (RoadWorkPhase phase in announcement.RoadWorkPhases)
                {
                    // check if roadwork is expired
                    TimeSpan ttl =   phase.TimeAndDuration.EndTime - DateTime.UtcNow;

                    if (ttl > TimeSpan.Zero)
                    {
                        Data.Redis.Entities.Roadwork roadwork = MapRoadworkPhaseToRoadwork(phase);
                    }
                }
            }
        }
    }

    public async Task<IEnumerable<Data.Redis.Entities.Roadwork>> FilterAsync(int primaryPointRoadNumber,
        int primaryPointRoadSection, int secondaryPointRoadNumber, int secondaryPointRoadSection,
        DateTime startTimeOnAfter, DateTime startTimeOnBefore, string severity)
    {
        // IEnumerable<Data.Redis.Entities.Roadwork> roadworkList = await _roadworkCacheRepository.FilterAsync(
        //    primaryPointRoadNumber, primaryPointRoadSection, secondaryPointRoadNumber, secondaryPointRoadSection,
        //    startTimeOnAfter, startTimeOnBefore, severity);

        return new List<Roadwork>();
    }

    private Data.Redis.Entities.Roadwork MapRoadworkPhaseToRoadwork(RoadWorkPhase phase)
    {
        RoadPoint? primaryPoint = phase.LocationDetails.RoadAddressLocation.PrimaryPoint;
        RoadPoint? secondaryPoint = phase.LocationDetails.RoadAddressLocation.SecondaryPoint;

        Data.Redis.Entities.Roadwork roadwork = new Data.Redis.Entities.Roadwork
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
            WorkingHours = phase.WorkingHours.Select(wh => new Data.Redis.Entities.WorkingHours
            {
                Weekday = wh.Weekday,
                StartTime = wh.StartTime,
                EndTime = wh.EndTime,
            }).ToList(),
            Restrictions = phase.Restrictions.Select(r => new Data.Redis.Entities.Restriction
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