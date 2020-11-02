using GenericBizRunner;
using ServicesLayer.Dto;

namespace ServicesLayer.Account
{
    public interface ILoginAccountAction : IGenericActionAsync<AccountDto, bool> { }
}
