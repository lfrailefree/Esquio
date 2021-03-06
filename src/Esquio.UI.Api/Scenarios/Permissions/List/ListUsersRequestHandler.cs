﻿using Esquio.UI.Api.Infrastructure.Data.DbContexts;
using Esquio.UI.Api.Infrastructure.Data.Entities;
using Esquio.UI.Api.Shared.Models;
using Esquio.UI.Api.Shared.Models.Permissions.List;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Esquio.UI.Api.Scenarios.Permissions.List
{
    public class ListUsersRequestHandler
        : IRequestHandler<ListPermissionRequest, PaginatedResult<ListUsersResponseDetail>>
    {
        private readonly StoreDbContext _storeDbContext;

        public ListUsersRequestHandler(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));
        }

        public async Task<PaginatedResult<ListUsersResponseDetail>> Handle(ListPermissionRequest request, CancellationToken cancellationToken = default)
        {
            var total = await _storeDbContext.Permissions
                .CountAsync(cancellationToken);

            var userPermissions = await _storeDbContext
                .Permissions
                .Where(p=>p.Kind == SubjectType.User)
                .Skip(request.PageIndex * request.PageCount)
                .Take(request.PageCount)
                .Select(u => new ListUsersResponseDetail()
                {
                    SubjectId = u.SubjectId,
                    ActAs = u.ApplicationRole.ToString(),
                }).ToListAsync(cancellationToken);

            return new PaginatedResult<ListUsersResponseDetail>()
            {
                Total = total,
                PageIndex = request.PageIndex,
                Count = userPermissions.Count,
                Items = userPermissions
            };
        }
    }
}
