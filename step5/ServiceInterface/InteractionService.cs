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
using SuperCRM.Request.Interactions;
using SuperCRM.Response;

namespace SuperCRM.ServiceInterface
{
    public class InteractionService : ServiceBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public InteractionService(IUserService<Guid, Guid, DbUser> userService,
            IServiceStackSecuritySettings securitySettings, AppDbContext dbContext, IConfig config, IMapper mapper)
            : base(userService, securitySettings, config)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<BaseRecordResponse<Interaction>> Post(CreateInteraction request)
        {
            var entity = mapper.Map<DbInteraction>(request);
            entity.CreatedById = this.UserService.CurrentUserId;
            this.dbContext.Interactions.Add(entity);
            await this.dbContext.SaveChangesAsync();
            return Ok(mapper.Map<Interaction>(entity));
        }

        public async Task<BaseResponse> Put(EditInteraction request)
        {
            var entity = await this.dbContext.Interactions.FindAsync(request.Id);
            if (entity == null)
                return Error(OpResult.DoNotExist, "Interaction not found.");

            mapper.Map(request, entity);
            await this.dbContext.SaveChangesAsync();
            return Ok(mapper.Map<Interaction>(entity));
        }

        public async Task<BaseResponse> Delete(DeleteInteraction request)
        {
            var entity = await this.dbContext.Interactions.FindAsync(request.InteractionId);
            if (entity == null)
                return Error(OpResult.DoNotExist, "Interaction not found.");

            this.dbContext.Remove(entity);
            await this.dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<BaseListResponse<Interaction>> Get(GetInteractions request)
        {
	        Expression <Func<DbInteraction, bool>> predicate;
            if (request.ContactId.HasValue)
                predicate = i => i.ContactId == request.ContactId.Value;
            else
                predicate = i => i.Contact.OwnerId == this.UserService.CurrentUser.OwnerUserId;

            var result = new
            {
	            Total = await this.dbContext.Interactions.CountAsync(predicate),
	            ThisPage = await this.dbContext.Interactions.Where(predicate)
		            .OrderByDescending(p => p.InteractionDate).Skip(request.StartIndex).Take(request.PageSize)
		            .AsQueryable()
		            .ProjectTo<Interaction>(mapper.ConfigurationProvider)
		            .ToListAsync()
            };

            return Ok(result.ThisPage, result.Total);
        }
    }
}
