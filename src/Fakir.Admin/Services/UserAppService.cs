﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Fakir.Admin.Dtos.Users;
using Fakir.Authorization.Roles;
using Fakir.Dto;
using System.Linq.Dynamic;
using Abp.AutoMapper;
using Fakir.Authorization;

namespace Fakir.Admin.Services
{
    public class UserAppService : FakirAppServiceBase, IUserAppService
    {
        private readonly RoleManager _roleManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public UserAppService(
            RoleManager roleManager,
            INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _roleManager = roleManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        public Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(IdInput<long> input)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdInput<long> input)
        {
            var userRoleDtos = (await _roleManager.Roles
              .OrderBy(r => r.DisplayName)
              .Select(r => new UserRoleDto
              {
                  RoleId = r.Id,
                  RoleName = r.Name,
                  RoleDisplayName = r.DisplayName
              })
              .ToArrayAsync());

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };

            if (!input.Id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto { IsActive = true, ShouldChangePasswordOnNextLogin = true };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);

                output.User = user.MapTo<UserEditDto>();
                output.ProfilePictureId = user.ProfilePictureId;

                foreach (var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(input.Id.Value, userRoleDto.RoleName);
                }
            }

            return output;
        }

        public Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IdInput<long> input)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultOutput<UserListDto>> GetUsers(GetUsersInput input)
        {
            var query = UserManager.Users
               .Include(u => u.Roles)
               .WhereIf(
                   !input.Filter.IsNullOrWhiteSpace(),
                   u =>
                       u.Name.Contains(input.Filter) ||
                       u.Surname.Contains(input.Filter) ||
                       u.UserName.Contains(input.Filter) ||
                       u.EmailAddress.Contains(input.Filter)
               );

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = users.MapTo<List<UserListDto>>();
            await FillRoleNames(userListDtos);

            return new PagedResultOutput<UserListDto>(
                userCount,
                userListDtos
                );
        }

        public Task<FileDto> GetUsersToExcel()
        {
            throw new NotImplementedException();
        }

        public Task ResetUserSpecificPermissions(IdInput<long> input)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
            await UserManager.SetGrantedPermissionsAsync(user, grantedPermissions);
        }

        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            /* This method is optimized to fill role names to given list. */

            var distinctRoleIds = (
                from userListDto in userListDtos
                from userListRoleDto in userListDto.Roles
                select userListRoleDto.RoleId
                ).Distinct();

            var roleNames = new Dictionary<int, string>();
            foreach (var roleId in distinctRoleIds)
            {
                roleNames[roleId] = (await _roleManager.GetRoleByIdAsync(roleId)).DisplayName;
            }

            foreach (var userListDto in userListDtos)
            {
                foreach (var userListRoleDto in userListDto.Roles)
                {
                    userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                }

                userListDto.Roles = userListDto.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }
    }
}
