using AutoMapper;
using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IStationRepository _stationRepository;
    private readonly IMapper _mapper;

    public UsersService(IMapper mapper, IUsersRepository usersRepository, IStationRepository stationRepository)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
        _stationRepository = stationRepository;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        IEnumerable<User> foundUsers = await _usersRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserResponse>>(foundUsers);
    }

    public async Task<bool> AddFavoriteStationsAsync(string id, string stationId)
    {
        User? foundUser = await _usersRepository.GetByIdAsync(id);

        if (foundUser == null)
        {
            throw new NotFoundException($"User {id} not found");
        }

        Station? foundStation = await _stationRepository.GetByStationIdAsync(stationId);

        if (foundStation == null)
        {
            throw new NotFoundException($"Station {stationId} not found");
        }

        if (foundUser.FavoriteStations.Contains(stationId))
        {
            throw new BadRequestException($"Station {stationId} is already user {id} favorite");
        }

        var updateDefinition = new UpdateDefinitionBuilder<User>()
            .Push(e=>e.FavoriteStations,stationId);

        return await _usersRepository.UpdateAsync(foundUser.Id,updateDefinition);
    }

    public async Task<IEnumerable<string>> GetFavoriteStationsAsync(string id)
    {
        User? foundUser = await _usersRepository.GetByIdAsync(id);

        if (foundUser == null)
        {
            throw new NotFoundException($"User {id} not found");
        }

        return foundUser.FavoriteStations;
    }

    public async Task<bool> RemoveFavoriteStationAsync(string id,string stationId)
    {
        User? foundUser = await _usersRepository.GetByIdAsync(id);

        if (foundUser == null)
        {
            throw new NotFoundException($"User {id} not found");
        }

        if (!foundUser.FavoriteStations.Contains(stationId))
        {
            throw new BadRequestException($"Station {stationId} is not user {id} favorite");
        }

        var updateDefinition = new UpdateDefinitionBuilder<User>()
            .Pull(e => e.FavoriteStations, stationId);

        return await _usersRepository.UpdateAsync(foundUser.Id, updateDefinition);
    }
}