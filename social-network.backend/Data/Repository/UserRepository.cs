using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using social_network.backend.Interfaces;
using social_network.backend.Entities.Identity;

namespace social_network.backend.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<MemberDTO> GetMemberAsync(string username)
        //{
        //    return await _context.Users.Where(x => x.UserName == username)
        //        .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
        //        .SingleOrDefaultAsync();
        //}

        //public async Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams)
        //{
        //    var query = _context.Users.AsQueryable();

        //    query = query.Where(u => u.UserName != userParams.CurrentUsername);
        //    query = query.Where(u => u.Gender == userParams.Gender);

        //    var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
        //    var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

        //    query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth < maxDob);

        //    query = userParams.OrderBy switch
        //    {
        //        "created" => query.OrderByDescending(u => u.Created),
        //        _ => query.OrderByDescending(u => u.LastActive),
        //    };

        //    return await PagedList<MemberDTO>.CreateAsync(query.AsNoTracking().ProjectTo<MemberDTO>(_mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);
        //}

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
