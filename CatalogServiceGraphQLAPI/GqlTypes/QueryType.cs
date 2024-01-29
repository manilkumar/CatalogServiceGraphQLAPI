using CatalogServiceGraphQLAPI.Data;
using CatalogServiceGraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogServiceGraphQLAPI.GqlTypes
{
    public class QueryType
    {
        public async Task<List<Category>> GetCategories([Service] CatalogDBContext context)
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById([Service] CatalogDBContext context, int id)
        {
            return await context.Categories.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Item>> GetItems([Service] CatalogDBContext context)
        {
            return await context.Items.ToListAsync();
        }

        public async Task<List<Item>> GetItemsWithPagination([Service] CatalogDBContext context, int categoryId, int start, int end)
        {
            return await context.Items.Where(i => i.CategoryId == categoryId).Skip(start).Take(end).ToListAsync();
        }

        public async Task<Item> GetItemById([Service] CatalogDBContext context, int id)
        {
            return await context.Items.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
    }
}
