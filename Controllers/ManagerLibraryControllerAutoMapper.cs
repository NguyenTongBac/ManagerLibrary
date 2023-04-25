using AutoMapper;
using Controllers.Dtos.Items;
using Controllers.Dtos.Borrowers;
using Models.Tables;
using Controllers.Dtos.BorrowHistories;
using Controllers.Dtos.Users;
using Controllers.Dtos.BorrowerHistoryDetails;

namespace Controllers;

public class ManagerLibraryControllerAutoMapper : Profile
{
    public ManagerLibraryControllerAutoMapper()
    {
        CreateMap<Item, ItemDto>();
        CreateMap<ItemCreateUpdateDto, Item>();
        CreateMap<Item, ItemCreateUpdateDto>();

        CreateMap<Borrower, BorrowerDto>();
        CreateMap<BorrowerCreateUpdateDto, Borrower>();
        CreateMap<Borrower, BorrowerCreateUpdateDto>();

        CreateMap<BorrowHistory, BorrowHistoryDto>()
        .ForMember(x => x.BorrowerName, y => y.MapFrom(u => u.Borrower.Name))
        .ForMember(x => x.Quantity, y => y.MapFrom(u => u.BorrowHistoryDetails.Sum(z => z.Quantity)))
        .ForMember(x => x.BorrowHistoryDetailDtos, y => y.MapFrom(z => z.BorrowHistoryDetails));
        CreateMap<BorrowHistoryCreateDto, BorrowHistory>().ForMember(x => x.BorrowHistoryDetails, y => y.MapFrom(u => u.BorrowHistoryDetailCreateDtos));

        CreateMap<BorrowHistoryDetail, BorrowHistoryDetailDto>().ForMember(x => x.ItemName, y => y.MapFrom(z => z.Item.Name));
        CreateMap<BorrowHistoryDetailCreateDto, BorrowHistoryDetail>();

        CreateMap<User, UserDto>();
        CreateMap<User, UserCreateUpdateDto>();
    }
}