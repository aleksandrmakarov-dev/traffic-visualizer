using AutoMapper;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Repositories.Database;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersService(IMapper mapper, IUsersRepository usersRepository)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        IEnumerable<User> foundUsers = await _usersRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserResponse>>(foundUsers);
    }
}