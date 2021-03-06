﻿namespace Chiota.Services.UserServices
{
  using System.Threading.Tasks;

  using Chiota.Messenger.Usecase;
  using Chiota.Messenger.Usecase.CreateUser;
  using Chiota.Models;

  using Tangle.Net.Entity;

  using TangleMessenger = Iota.TangleMessenger;

  /// <inheritdoc />
  public class UserFactory : IUserFactory
  {
    public UserFactory(IUsecaseInteractor<CreateUserRequest, CreateUserResponse> createUserInteractor)
    {
      this.CreateUserInteractor = createUserInteractor;
    }

    private IUsecaseInteractor<CreateUserRequest, CreateUserResponse> CreateUserInteractor { get; }

    /// <inheritdoc />
    public async Task<User> CreateAsync(Seed seed, string name)
    {
      var response = await this.CreateUserInteractor.ExecuteAsync(new CreateUserRequest { Seed = seed });

      return new User
               {
                 Name = name,
                 Seed = seed.Value,
                 ImageHash = null,
                 StoreSeed = true,
                 PublicKeyAddress = response.PublicKeyAddress.Value, 
                 RequestAddress = response.RequestAddress.Value,
                 TangleMessenger = new TangleMessenger(seed),
                 NtruKeyPair = response.NtruKeyPair
               };
    }
  }
}
