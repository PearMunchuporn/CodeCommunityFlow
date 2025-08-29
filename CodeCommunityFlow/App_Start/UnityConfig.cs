using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.ServiceLayers;
using CodeCommunityFlow.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CodeCommunityFlow.ServiceLayers.ProfilesMapping;
using Unity.Lifetime;

namespace CodeCommunityFlow.App_Start
{
    public class UnityConfig
    {
        public static UnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
            Container = new UnityContainer(); // กำหนดก่อนเลย

            Container.RegisterType<IQuestionService, QuestionService>();
            Container.RegisterType<CodeCommunityFlowDbContext>();
            Container.RegisterType<IUserService, UserService>();
            Container.RegisterType<ICategoryService, CategoryService>();
            Container.RegisterType<IAnswerService, AnswerService>();
            Container.RegisterType<IHowknowService, HowKnowUsService>();
            Container.RegisterType<IContactService, ContactService>();
            Container.RegisterType<IReportService, ReportService>();
            Container.RegisterType<IReportLogsService, ReportLogsService>();
            Container.RegisterType<IDeleteLogsService, DeleteLogsService>();
            Container.RegisterType<IScoreHistoryLogsService, ScoreHistoryServiceLogs>();
            Container.RegisterType<IThankLogsService, ThanksLogService>();
            Container.RegisterType<IAdminAnnounceService, AdminAnnouncementService>();
            Container.RegisterType<IAdminUserService, AdminUsersService>();
            Container.RegisterType<IAdminLogsActionService, AdminActionLogsService>();
            Container.RegisterType<ICommentFromAnnouncementService, CommentFromAnnouncementService>();
            Container.RegisterInstance<ILoggerFactory>(LoggerFactory.Create(builder => { }));
            var loggerFactory = Container.Resolve<ILoggerFactory>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<ScoreHistoryLogsMappingProfile>();
                cfg.AddProfile<AdminAnnouncementMappingProfile>();
                cfg.AddProfile<AdminUsersMappingProfile>();
                cfg.AddProfile<CategoryMappingProfile>();
                cfg.AddProfile<CommentFromAnnouncementMappingProfile>();
                cfg.AddProfile<ContactMappingProfile>();
                cfg.AddProfile<DeleteLogsMappingProfile>();
                cfg.AddProfile<HowKnowUsMappingProfile>();
                cfg.AddProfile<QuestionsMappingProfile>();
                cfg.AddProfile<ReportLogsMappingProfile>();
                cfg.AddProfile<ReportMappingProfile>();
                cfg.AddProfile<ThankLogsMappingProfile>();
                cfg.AddProfile<AdminActionLogsMappingProfile>();
                cfg.AddProfile<AnswersMappingProfile>();
            }, loggerFactory);

            Container.RegisterInstance<IMapper>(mapperConfig.CreateMapper());
            Container.RegisterInstance<IMapper>(
            mapperConfig.CreateMapper(),
            new ContainerControlledLifetimeManager()
);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(Container);
         
        }
    }
}