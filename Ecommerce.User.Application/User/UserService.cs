using AutoMapper;
using Ecommerce.User.Application.Shared;
using Ecommerce.User.Application.User.Dto;
using Ecommerce.User.Domain.Entity.Readonly.Repository;
using Ecommerce.User.Domain.Entity.User.Repository;

namespace Ecommerce.User.Application.User
{
    //public class UserService : IUserService
    public class UserService : AbstractService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReadonlyRepository _readonlyRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IReadonlyRepository readonlyRepository)
            : base(mapper)
        {
            _userRepository = userRepository;
            //_mapper = mapper;
            _readonlyRepository = readonlyRepository;
        }
        public async Task<UserDto> GetUser(int userId)
        {
            var result = await _userRepository.GetOneByCriteria(a => a.Id == userId);
            return _mapper.Map<UserDto>(result);
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var result = await _readonlyRepository.GetAllUser();
            return _mapper.Map<List<UserDto>>(result);
        }

        //public async Task<UserDto> SaveUser(UserDto userDto)
        //{
        //    var entity = _mapper.Map<Ecommerce.User.Domain.Entity.User.User>(userDto);
        //    try
        //    {
        //        if (userDto.Id > 0)
        //            await _userRepository.Update(entity);
        //        else
        //            await _userRepository.Save(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //            throw ex.InnerException;
        //        throw;
        //    }
        //    return _mapper.Map<UserDto>(entity);
        //}
        public async Task<UserDto> SaveUser(UserDto userDto)
        {
            using (var transaction = await _userRepository.CreateTransaction())
            {
                try
                {
                    //if (!userDto.Created.HasValue)
                    //    Created.Created = DateTime.Now;

                    var result = await SaveUpdateDeleteDto(userDto, _userRepository);

                    await transaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task<bool> DeleteUser(int userdId)
        {
            using (var transaction = await _userRepository.CreateTransaction())
            {
                try
                {
                    var user = await _userRepository.GetOneByCriteria(a => a.Id == userdId);

                    await _userRepository.Delete(user);

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
