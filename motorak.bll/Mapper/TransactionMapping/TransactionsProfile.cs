

using AutoMapper;
using motorak.dal.Entities;
using Motorak.BLL.ModelVM.Purchases;
using Motorak.BLL.ModelVM.Rents;
using Motorak.BLL.ModelVM.Transactions;
using Motorak.DAL.Entities;

namespace Motorak.BLL.Mapper.TransactionMapping
{
    public class TransactionsProfile:Profile
    {
        public TransactionsProfile() {
            CreateMap<Transactions, TransactionCreateDto>();
            CreateMap<TransactionCreateDto, Transactions>();
            CreateMap<Transactions, TransactionUpdateDto>();
            CreateMap<TransactionUpdateDto, Transactions>();

            CreateMap<Rent, RentCreateDto>();
            CreateMap<RentCreateDto, Rent>();
            CreateMap<Rent, RentUpdateDto>();
            CreateMap<RentUpdateDto, Rent>();

            CreateMap<Purchase, PurchaseCreateDto>();
            CreateMap<PurchaseCreateDto, Purchase>();
            CreateMap<Purchase, PurchaseUpdateDto>();
            CreateMap<PurchaseUpdateDto, Purchase>();


            CreateMap<Transactions, TransactionReadDto>();
            CreateMap<TransactionCreateDto, Transactions>();

            CreateMap<Purchase, PurchaseReadDto>();
            CreateMap<PurchaseCreateDto, Purchase>();

            CreateMap<Rent, RentReadDto>();
            CreateMap<RentCreateDto, Rent>();




        }
    }
}
