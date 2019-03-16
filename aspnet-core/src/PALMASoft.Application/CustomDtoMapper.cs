using PALMASoft.ASuelos.Dtos;
using PALMASoft.ASuelos;
using PALMASoft.AFoliares.Dtos;
using PALMASoft.AFoliares;
using PALMASoft.Analises.Dtos;
using PALMASoft.Analises;
using PALMASoft.Fincas.Dtos;
using PALMASoft.Fincas;
using PALMASoft.Clientes.Dtos;
using PALMASoft.Clientes;
using PALMASoft.Municipios.Dtos;
using PALMASoft.Municipios;
using PALMASoft.Departamentos.Dtos;
using PALMASoft.Departamentos;
using PALMASoft.Paises.Dtos;
using PALMASoft.Paises;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using PALMASoft.Auditing.Dto;
using PALMASoft.Authorization.Accounts.Dto;
using PALMASoft.Authorization.Permissions.Dto;
using PALMASoft.Authorization.Roles;
using PALMASoft.Authorization.Roles.Dto;
using PALMASoft.Authorization.Users;
using PALMASoft.Authorization.Users.Dto;
using PALMASoft.Authorization.Users.Profile.Dto;
using PALMASoft.Chat;
using PALMASoft.Chat.Dto;
using PALMASoft.Editions;
using PALMASoft.Editions.Dto;
using PALMASoft.Friendships;
using PALMASoft.Friendships.Cache;
using PALMASoft.Friendships.Dto;
using PALMASoft.Localization.Dto;
using PALMASoft.MultiTenancy;
using PALMASoft.MultiTenancy.Dto;
using PALMASoft.MultiTenancy.HostDashboard.Dto;
using PALMASoft.MultiTenancy.Payments;
using PALMASoft.MultiTenancy.Payments.Dto;
using PALMASoft.Notifications.Dto;
using PALMASoft.Organizations.Dto;
using PALMASoft.Sessions.Dto;

namespace PALMASoft
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
           configuration.CreateMap<CreateOrEditASueloDto, ASuelo>();
           configuration.CreateMap<ASuelo, ASueloDto>();
           configuration.CreateMap<CreateOrEditAFoliarDto, AFoliar>();
           configuration.CreateMap<AFoliar, AFoliarDto>();
           configuration.CreateMap<CreateOrEditAnalisisDto, Analisis>();
           configuration.CreateMap<Analisis, AnalisisDto>();
           configuration.CreateMap<CreateOrEditFincaDto, Finca>();
           configuration.CreateMap<Finca, FincaDto>();
           configuration.CreateMap<CreateOrEditClienteDto, Cliente>();
           configuration.CreateMap<Cliente, ClienteDto>();
           configuration.CreateMap<CreateOrEditMunicipioDto, Municipio>();
           configuration.CreateMap<Municipio, MunicipioDto>();
           configuration.CreateMap<CreateOrEditDepartamentoDto, Departamento>();
           configuration.CreateMap<Departamento, DepartamentoDto>();
           configuration.CreateMap<CreateOrEditPaisDto, Pais>();
           configuration.CreateMap<Pais, PaisDto>();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>(); 

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}