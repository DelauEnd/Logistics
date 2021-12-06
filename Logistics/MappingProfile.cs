using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Logistics
{
    public class MappingProfile : Profile
    {
        private IRepositoryManager _repository;
        protected IRepositoryManager repository => _repository ?? (_repository = App.ServiceProvider.GetService<IRepositoryManager>());

        public MappingProfile()
        {
            CreateTruckMaps();
            CreateTrailerMaps();
            CreateCargoMaps();
            CreateRouteMaps();
            CreateOrderMaps();
            CreateCustomerMaps();
            CreateCargoTypeMaps();
            CreateUserMaps();
        }

        private void CreateCargoTypeMaps()
        {
            CreateMap<CargoType, CargoTypeDto>().ReverseMap();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserWithLoginDto>().ForMember(userDto => userDto.Login, option
                  => option.MapFrom(user => user.AccountInfo.Login));
        }

        private void CreateTrailerMaps()
        {
            CreateMap<Trailer, TrailerDto>().ForMember(truck => truck.TransportedCargoType, option
                  => option.MapFrom(truckDto => truckDto.TransportedCargoType.Title));

            CreateMap<TrailerForCreationDto, Trailer>();
        }

        private void CreateTruckMaps()
        {
            CreateMap<Truck, TruckDto>().ForMember(truck=>truck.TransportedCargoType, option
                => option.MapFrom(truckDto => truckDto.TransportedCargoType.Title));

            CreateMap<TruckForCreationDto, Truck>();
        }

        private void CreateCargoMaps()
        {
            CreateMap<Cargo, CargoDto>()
                .ForMember(cargoDto => cargoDto.Type, option =>
                option.MapFrom(cargo => cargo.Type.Title)).ReverseMap();

            CreateMap<CargoForCreationDto, Cargo>().ReverseMap();
        }

        private void CreateRouteMaps()
        {
            CreateMap<Route, RouteDto>()
                .ForMember(routeDto => routeDto.TruckRegistrationNumber, option 
                => option.MapFrom(transport => transport.Truck.RegistrationNumber))
                .ForMember(routeDto => routeDto.TrailerRegistrationNumber, option => 
                option.MapFrom(transport => transport.Trailer.RegistrationNumber));

            CreateMap<RouteForCreationDto, Route>().ReverseMap();
        }

        private void CreateOrderMaps()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(orderDto => orderDto.Sender, option =>
                option.MapFrom(order => order.Sender.Address))
                .ForMember(orderDto => orderDto.Destination, option =>
                option.MapFrom(order => order.Destination.Address)).ReverseMap();

            CreateMap<OrderForCreationDto, Order>()
                .ForMember(order => order.Status, option =>
                option.MapFrom(orderForCreation => Status.Processing));          
        }

        private void CreateCustomerMaps()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerForCreationDto, Customer>();
        }
    }
}
