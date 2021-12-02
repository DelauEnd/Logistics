using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ObjectsForUpdate;
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
            CreateUserMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDto>();
        }

        private void CreateTrailerMaps()
        {
            CreateMap<Trailer, TrailerDto>().ForMember(truck => truck.TransportedCargoType, option
                  => option.MapFrom(truckDto => truckDto.TransportedCargoType.Title));
        }

        private void CreateTruckMaps()
        {
            CreateMap<Truck, TruckDto>().ForMember(truck=>truck.TransportedCargoType, option
                => option.MapFrom(truckDto => truckDto.TransportedCargoType.Title));

            CreateMap<TruckForCreationDto, Truck>();

            CreateMap<TruckForUpdateDto, Truck>().ReverseMap();
        }

        private void CreateCargoMaps()
        {
            CreateMap<Cargo, CargoDto>()
                .ForMember(cargoDto => cargoDto.Type, option =>
                option.MapFrom(cargo => cargo.Type.Title)).ReverseMap();

            CreateMap<CargoForUpdateDto, Cargo>().ReverseMap();

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
                option.MapFrom(orderForCreation => EStatuses.PROCESSING));

            CreateMap<OrderForUpdateDto, Order>()
                .ForMember(order => order.Status, option =>
                option.MapFrom(order => 
                    Enum.IsDefined(typeof(EStatuses), order.Status) ?
                    Enum.Parse(typeof(EStatuses), order.Status) :
                    EStatuses.PROCESSING))
                .ReverseMap()
                .ForMember(updateOrder => updateOrder.Status, option  =>  
                option.MapFrom(order => order.Status.ToString()));
        }

        private void CreateCustomerMaps()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerForCreation, Customer>();

            CreateMap<CustomerForUpdateDto, Customer>().ReverseMap();
        }
    }
}
