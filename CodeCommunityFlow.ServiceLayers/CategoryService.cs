using CodeCommunityFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeCommunityFlow.DomainModels;
using CodeCommunityFlow.Repository;
using AutoMapper;

namespace CodeCommunityFlow.ServiceLayers
{

    public interface ICategoryService
    {
        void InsertCategory(CategoryViewModel categoryViewModel);
        void UpdateCategory(CategoryViewModel categoryViewModel);
        void DeleteCategory(int categoryID);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByID(int categoryID);
    }
      public class CategoryService : ICategoryService
     {
        ICategoryRepository categoryRepository;
        IMapper _mapper;
        public CategoryService(IMapper mapper)
        {
            categoryRepository = new CategoryRepository();
            _mapper = mapper;
        }


        public List<CategoryViewModel> GetCategories()
        {
            var categories = categoryRepository.GetAllCategory();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }

        public CategoryViewModel GetCategoryByID(int categoryId)
        {
            var category = categoryRepository.GetCategoryByID(categoryId);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public void InsertCategory(CategoryViewModel categoryViewModel)
        {
            var catogoryInsert = _mapper.Map<Categories>(categoryViewModel);
            categoryRepository.InsertCategory(catogoryInsert);
        }

        public void UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var catogoryUpdate = _mapper.Map<Categories>(categoryViewModel);
            categoryRepository.UpdateCategory(catogoryUpdate);
        }

        public void DeleteCategory(int CategoryID)
        {
            categoryRepository.DeleteCategory(CategoryID);
        }

    }
}
