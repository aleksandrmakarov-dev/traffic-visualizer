using AutoMapper;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpRequest, User>();

            CreateMap<NewEmailVerificationRequest, User>();

            CreateMap<User, UserResponse>();

            CreateMap<User, EmailVerificationResponse>();
        }
    }
}
