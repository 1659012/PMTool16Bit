using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PMTool16Bit.Authorization;
using PMTool16Bit.Services;
using PMTool16Bit.Models;
namespace PMTool16Bit
{
    [DependsOn(
        typeof(PMTool16BitCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class PMTool16BitApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<PMTool16BitAuthorizationProvider>();

        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PMTool16BitApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => {
                    cfg.AddProfiles(thisAssembly);

                    //cfg.CreateMap<Foo, FooCopy>()
                    //    .ForMember(x =>x.Text, opt => opt.Ignore())
                    //    .ForMember(x => x.Age , opt => opt.Ignore() );

                    cfg.CreateMap<ProjectCreateDto, Project>()
                        .ForMember(x => x.ProjectOwner, opt => opt.Ignore())
                        .ForMember(x => x.TaskGroups, opt => opt.Ignore())
                        ;

                    cfg.CreateMap<ProjectUpdateDto, Project>()
                        .ForMember(x => x.ProjectOwner, opt => opt.Ignore())
                        .ForMember(x => x.TaskGroups, opt => opt.Ignore())
                        ;

                    cfg.CreateMap<ProjectMemberDto, ProjectMember>()
                        .ForMember(x => x.Id, opt => opt.Ignore())
                        .ForMember(x => x.Project, opt => opt.Ignore())
                        .ForMember(x => x.Member, opt => opt.Ignore())
                        ;

                    cfg.CreateMap<TaskGroupCreateDto, TaskGroup>()
                     .ForMember(x => x.Project, opt => opt.Ignore())
                     .ForMember(x => x.EventTasks, opt => opt.Ignore())
                     ;

                    cfg.CreateMap<EventTaskCreateDto, EventTask>()
                    .ForMember(x => x.GroupTask, opt => opt.Ignore())
                    ;

                    cfg.CreateMap<EventTaskMemberDto, EventTaskMember>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    ;

                }
            );
        }
       
    }
}
