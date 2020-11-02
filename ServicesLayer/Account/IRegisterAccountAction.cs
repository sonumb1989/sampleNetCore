using GenericBizRunner;
using ServicesLayer.Dto;

namespace ServicesLayer.Account
{
    public interface IRegisterAccountAction : IGenericActionAsync<AccountDto, bool> { }
}
