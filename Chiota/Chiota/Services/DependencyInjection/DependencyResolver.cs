﻿namespace Chiota.Services.DependencyInjection
{
  using System;
  using System.Collections.Generic;

  using Autofac;
  using Autofac.Core;

  using Chiota.Messenger.Service;
  using Chiota.Messenger.Usecase;
  using Chiota.Messenger.Usecase.AcceptContact;
  using Chiota.Messenger.Usecase.AddContact;
  using Chiota.Messenger.Usecase.CreateUser;
  using Chiota.Messenger.Usecase.DeclineContact;
  using Chiota.Messenger.Usecase.GetContacts;
  using Chiota.Messenger.Usecase.SendMessage;
  using Chiota.Services.Iota.Repository;
  using Chiota.Services.UserServices;
  using Chiota.ViewModels;
  using Chiota.ViewModels.Classes;

  using Tangle.Net.Cryptography;
  using Tangle.Net.Repository;

  public static class DependencyResolver
  {
    static DependencyResolver()
    {
      Modules = new List<IModule>();
    }

    public static List<IModule> Modules { get; set; }

    private static IContainer Container { get; set; }

    public static void Init()
    {
      var builder = new ContainerBuilder();

      builder.RegisterModule(new ViewModelInjectionModule());
      foreach (var module in Modules)
      {
        builder.RegisterModule(module);
      }

      builder.RegisterType<UserFactory>().As<IUserFactory>();
      builder.RegisterType<UserService>().PropertiesAutowired();
      builder.RegisterInstance(new RepositoryFactory().Create()).As<IIotaRepository>();
      builder.RegisterType<AddressGenerator>().As<IAddressGenerator>();

      builder.RegisterType<TangleMessenger>().As<IMessenger>().PropertiesAutowired();

      builder.RegisterType<AddContactInteractor>().As<IUsecaseInteractor<AddContactRequest, AddContactResponse>>().PropertiesAutowired();
      builder.RegisterType<GetContactsInteractor>().As<IUsecaseInteractor<GetContactsRequest, GetContactsResponse>>()
        .PropertiesAutowired();
      builder.RegisterType<AcceptContactInteractor>().As<IUsecaseInteractor<AcceptContactRequest, AcceptContactResponse>>().PropertiesAutowired();
      builder.RegisterType<DeclineContactInteractor>().As<IUsecaseInteractor<DeclineContactRequest, DeclineContactResponse>>().PropertiesAutowired();

      builder.RegisterType<SendMessageInteractor>().As<IUsecaseInteractor<SendMessageRequest, SendMessageResponse>>().PropertiesAutowired();

      builder.RegisterType<CreateUserInteractor>().As<IUsecaseInteractor<CreateUserRequest, CreateUserResponse>>().PropertiesAutowired();

      builder.RegisterType<UserService>().As<UserService>().PropertiesAutowired();
      Container = builder.Build();
    }

    public static T Resolve<T>()
    {
      if (Container == null)
      {
        Init();
      }

      return Container.Resolve<T>();
    }

    public static object Resolve(Type type)
    {
      if (Container == null)
      {
        Init();
      }

      return Container.Resolve(type);
    }

    public static void Reload()
    {
      Init();
    }
  }
}