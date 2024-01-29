using CatalogServiceGraphQLAPI.Data;
using CatalogServiceGraphQLAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogServiceGraphQLAPI.MutationType
{
    public class MutationType
    {
        public async Task SaveCategory([Service] CatalogDBContext context, Category newCategory)
        {
            context.Categories.Add(newCategory);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategory([Service] CatalogDBContext context, Category updateCategory)
        {
            context.Categories.Update(updateCategory);
            await context.SaveChangesAsync();
        }

        public async Task<string> DeleteCategory([Service] CatalogDBContext context, int id)
        {
            var relatedItemsToDelete = await context.Items.Where(i => i.CategoryId == id).ToListAsync();
            context.RemoveRange(relatedItemsToDelete);

            var categoryToDelete = await context.Categories.FindAsync(id);
            if (categoryToDelete == null)
            {
                return "Invalid Operation";
            }
            context.Categories.Remove(categoryToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }

        public async Task SaveItem([Service] CatalogDBContext context, Item newItem)
        {
            context.Items.Add(newItem);
            await context.SaveChangesAsync();
        }

        public async Task UpdateItem([Service] CatalogDBContext context, Item updateItem)
        {
            context.Items.Update(updateItem);
            await context.SaveChangesAsync();
        }

        public async Task<string> DeleteItem([Service] CatalogDBContext context, int id)
        {
            var itemToDelete = await context.Items.FindAsync(id);
            if (itemToDelete == null)
            {
                return "Invalid Operation";
            }
            context.Items.Remove(itemToDelete);
            await context.SaveChangesAsync();
            return "Record Deleted!";
        }
    }
}
