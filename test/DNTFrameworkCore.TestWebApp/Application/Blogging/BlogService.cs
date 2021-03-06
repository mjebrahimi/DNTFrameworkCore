using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTFrameworkCore.Application.Models;
using DNTFrameworkCore.Application.Services;
using DNTFrameworkCore.EntityFramework.Application;
using DNTFrameworkCore.EntityFramework.Context;
using DNTFrameworkCore.Eventing;
using DNTFrameworkCore.Functional;
using DNTFrameworkCore.TestWebApp.Application.Blogging.Models;
using DNTFrameworkCore.TestWebApp.Domain.Blogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DNTFrameworkCore.TestWebApp.Application.Blogging
{
    public interface IBlogService : ICrudService<int, BlogModel>
    {
    }

    public class BlogService : CrudService<Blog, int, BlogModel>, IBlogService
    {
        private readonly ILogger<BlogService> _logger;

        public BlogService(CrudServiceDependency dependency, ILogger<BlogService> logger) : base(dependency)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override IQueryable<BlogModel> BuildReadQuery(FilteredPagedQueryModel model)
        {
            _logger.LogInformation(nameof(BuildReadQuery));

            return EntitySet.AsNoTracking().Select(b => new BlogModel
                {Id = b.Id, RowVersion = b.RowVersion, Url = b.Url, Title = b.Title});
        }

        protected override Blog MapToEntity(BlogModel model)
        {
            _logger.LogInformation(nameof(MapToEntity));

            return new Blog
            {
                Id = model.Id,
                RowVersion = model.RowVersion,
                Url = model.Url,
                Title = model.Title,
                NormalizedTitle = model.Title.ToUpperInvariant() //todo: normalize based on your requirement 
            };
        }

        protected override BlogModel MapToModel(Blog entity)
        {
            _logger.LogInformation(nameof(MapToModel));

            return new BlogModel
            {
                Id = entity.Id,
                RowVersion = entity.RowVersion,
                Url = entity.Url,
                Title = entity.Title
            };
        }

        protected override Task AfterFindAsync(IReadOnlyList<BlogModel> models)
        {
            _logger.LogInformation(nameof(AfterFindAsync));

            return Task.CompletedTask;
        }

        protected override Task AfterMappingAsync(IReadOnlyList<Blog> entities, IReadOnlyList<BlogModel> models)
        {
            _logger.LogInformation(nameof(AfterMappingAsync));

            return Task.CompletedTask;
        }

        protected override Task<Result> BeforeCreateAsync(IReadOnlyList<BlogModel> models)
        {
            _logger.LogInformation(nameof(BeforeCreateAsync));

            return Task.FromResult(Ok());
        }

        protected override Task<Result> AfterCreateAsync(IReadOnlyList<BlogModel> models)
        {
            _logger.LogInformation(nameof(AfterCreateAsync));

            return Task.FromResult(Ok());
        }

        protected override Task<Result> BeforeEditAsync(
            IReadOnlyList<ModifiedModel<BlogModel>> models)
        {
            _logger.LogInformation(nameof(BeforeEditAsync));

            return Task.FromResult(Ok());
        }

        protected override Task<Result> AfterEditAsync(
            IReadOnlyList<ModifiedModel<BlogModel>> models)
        {
            _logger.LogInformation(nameof(AfterEditAsync));

            return Task.FromResult(Ok());
        }

        protected override Task<Result> BeforeDeleteAsync(IReadOnlyList<BlogModel> models)
        {
            _logger.LogInformation(nameof(BeforeDeleteAsync));

            return Task.FromResult(Ok());
        }

        protected override Task<Result> AfterDeleteAsync(IReadOnlyList<BlogModel> models)
        {
            _logger.LogInformation(nameof(AfterDeleteAsync));

            return Task.FromResult(Ok());
        }
    }
}