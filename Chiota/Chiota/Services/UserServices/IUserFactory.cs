﻿namespace Chiota.Services.UserServices
{
  using System.Threading.Tasks;

  using Chiota.Models;

  using Tangle.Net.Entity;

  /// <summary>
  /// The UserFactory interface.
  /// </summary>
  public interface IUserFactory
  {
    /// <summary>
    /// The create.
    /// </summary>
    /// <param name="seed">
    /// The seed.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="User"/>.
    /// </returns>
    Task<User> CreateAsync(Seed seed, string name);
  }
}