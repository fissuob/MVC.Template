﻿using AutoMapper;
using MvcTemplate.Objects;

namespace MvcTemplate.Data.Mapping
{
    public static class ObjectMapper
    {
        public static void MapObjects()
        {
            MapAccounts();
            MapRoles();
        }

        #region Administration

        private static void MapAccounts()
        {
            Mapper.CreateMap<Account, AccountView>();

            Mapper.CreateMap<Account, AccountEditView>();

            Mapper.CreateMap<Account, ProfileEditView>();

            Mapper.CreateMap<AccountCreateView, Account>();

            Mapper.CreateMap<AccountRegisterView, Account>();
        }

        private static void MapRoles()
        {
            Mapper.CreateMap<Role, RoleView>();
            Mapper.CreateMap<RoleView, Role>();
        }

        #endregion
    }
}
