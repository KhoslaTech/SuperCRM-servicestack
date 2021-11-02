using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ASPSecurityKit;
using ASPSecurityKit.ServiceStack;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SuperCRM.DataModels;
using SuperCRM.Models;
using SuperCRM.Request.Contacts;
using SuperCRM.Response;

namespace SuperCRM.ServiceInterface
{
    public class ContactService : ServiceBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public ContactService(IUserService<Guid, Guid, DbUser> userService,
            IServiceStackSecuritySettings securitySettings, AppDbContext dbContext, IConfig config, IMapper mapper)
            : base(userService, securitySettings, config)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<BaseRecordResponse<Contact>> Post(CreateContact request)
        {
            var entity = mapper.Map<DbContact>(request);
            entity.OwnerId = this.UserService.CurrentUser.OwnerUserId;
            entity.CreatedById = this.UserService.CurrentUserId;
            this.dbContext.Contacts.Add(entity);
            await this.dbContext.SaveChangesAsync();
            return Ok(mapper.Map<Contact>(entity));
        }

        public async Task<BaseResponse> Put(EditContact request)
        {
            var entity = await this.dbContext.Contacts.FindAsync(request.Id);
            if (entity == null)
                return Error(OpResult.DoNotExist, "Contact not found.");

            mapper.Map(request, entity);
            await this.dbContext.SaveChangesAsync();
            return Ok(mapper.Map<Contact>(entity));
        }

        public async Task<BaseResponse> Delete(DeleteContact request)
        {
            var entity = await this.dbContext.Contacts.Include(x => x.Interactions)
                .SingleOrDefaultAsync(x => x.Id == request.ContactId);
            if (entity == null)
                return Error(OpResult.DoNotExist, "Contact not found.");

            this.dbContext.Remove(entity);
            await this.dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<BaseListResponse<Contact>> Get(GetContacts request)
        {
            Expression<Func<DbContact, bool>> predicate = c => c.OwnerId == this.UserService.CurrentUser.OwnerUserId;

            var result = new
            {
	            Total = await this.dbContext.Contacts.CountAsync(predicate),
	            ThisPage = await this.dbContext.Contacts.Where(predicate)
		            .OrderBy(p => p.Name).Skip(request.StartIndex).Take(request.PageSize)
		            .AsQueryable()
		            .ProjectTo<Contact>(mapper.ConfigurationProvider)
		            .ToListAsync()
            };

            return Ok(result.ThisPage, result.Total);
        }
    }
}
