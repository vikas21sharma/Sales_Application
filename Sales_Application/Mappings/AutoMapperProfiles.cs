using AutoMapper;
using Sales_Application.DTO;
using Sales_Application.Models;

namespace Sales_Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Shipper, ShipperDto>().ReverseMap();
            CreateMap<Shipper, ShipperPutDto>().ReverseMap();
            CreateMap<Territory, TerritoryDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Order, OrdersDto>().ReverseMap();
            CreateMap<Order, ShipDetailsDtoById>().ReverseMap();
            CreateMap<Order, ShipDetailsDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        }
    }
}
