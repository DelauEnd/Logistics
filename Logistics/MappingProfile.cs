using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ObjectsForUpdate;
using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Logistics
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateTruckMaps();
            CreateCargoMaps();
            CreateRouteMaps();
            CreateOrderMaps();
            CreateCustomerMaps();
            CreateCargoCategoryMaps();
            CreateUserMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDto>();
        }

        private void CreateTruckMaps()
        {
            CreateMap<Truck, TruckDto>();

            CreateMap<TruckForCreationDto, Truck>();

            CreateMap<TruckForUpdateDto, Truck>().ReverseMap();
        }

        private void CreateCargoMaps()
        {
            CreateMap<Cargo, CargoDto>()
                .ForMember(cargoDto => cargoDto.Category, option =>
                option.MapFrom(cargo => cargo.Category.Title))
                .ForMember(cargoDto => cargoDto.Type, option =>
                option.MapFrom(cargo => cargo.Type.Title)).ReverseMap();

            CreateMap<CargoForUpdateDto, Cargo>().ReverseMap();

            CreateMap<CargoForCreationDto, Cargo>();
        }

        private void CreateRouteMaps()
        {
            CreateMap<Route, RouteDto>()
                .ForMember(routeDto => routeDto.TruckRegistrationNumber, option 
                => option.MapFrom(transport => transport.Truck.RegistrationNumber))
                .ForMember(routeDto => routeDto.TrailerRegistrationNumber, option => 
                option.MapFrom(transport => transport.Trailer.RegistrationNumber));
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

        private void CreateCargoCategoryMaps()
        {
            CreateMap<CargoCategory, CargoCategoryDto>();

            CreateMap<CategoryForCreationDto, CargoCategory>();

            CreateMap<CargoCategoryForUpdateDto, CargoCategory>().ReverseMap();
        }
    }
}
