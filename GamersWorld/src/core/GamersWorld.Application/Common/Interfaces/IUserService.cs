using GamersWorld.Application.Dtos.User;
using GamersWorld.Domain.Entities;

namespace GamersWorld.Application.Common.Interfaces;

public interface IUserService{
    AuthenticationResponse Authenticate(AuthenticationRequest request);
    User GetById(int userId);
}