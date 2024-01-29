using CatalogServiceGraphQLAPI.Entities;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CatalogServiceGraphQLAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> logger;
        private readonly IRequestExecutor requestExecutor;
        public CatalogController(ILogger<CatalogController> logger,
            IRequestExecutor requestExecutor)
        {
            this.logger = logger;
            this.requestExecutor = requestExecutor;
        }

        // GET: api/<CatalogController>
        [HttpGet]
        [Route("Category/GetCategory/{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("Invalid Category Id");
            }

            try
            {
                var query = @"query categoryById(id:$id){
                                      id
                                      name
                                      imageURL
                                }";

                
                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValue("id",categoryId)
                    .Create();


                var result = await requestExecutor.ExecuteAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/<CatalogController>
        [HttpGet]
        [Route("Category/GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var query = @"query categories{
                                      id
                                      name
                                      imageURL
                                }";


                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .Create();



                var result = await requestExecutor.ExecuteAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Category/AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category entity)
        {
            try
            {
                var query = @"mutation saveCategory(($input: Category!)) {
                          saveCategory(input: $input) {
                              id
                              name
                              imageURL
                            }
                            error
                          }";

                var variables = new Dictionary<string, object?>();

                variables.Add("name", entity.Name);
                variables.Add("imageURL", entity.ImageURL);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();

                await requestExecutor.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Category/UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category entity)
        {
            try
            {
                var query = @"mutation updateCategory(($input: Category!)) {
                          updateCategory(input: $input) {
                              id
                              name
                              imageURL
                            }
                            error
                          }";

                var variables = new Dictionary<string, object?>();

                variables.Add("id", entity.Id);
                variables.Add("name", entity.Name);
                variables.Add("imageURL", entity.ImageURL);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();

                await requestExecutor.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // DELETE api/<CatalogController>/delete/5
        [HttpDelete("Category/DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                var query = @"mutation deleteCategory(($input: int)) {
                          deleteCategory(input: $input) {
                              id
                            }
                            error
                          }";

                var variables = new Dictionary<string, object?>();

                variables.Add("id", categoryId);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();

                await requestExecutor.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // GET: api/<CatalogController>
        [HttpGet]
        [Route("Item/GetItem/{categoryId}/{itemId}")]
        public async Task<IActionResult> GetItem(int itemId)
        {
            if (itemId <= 0)
            {
                return BadRequest("Invalid Item Id");
            }

            try
            {
                var query = @"query itemById(id:$id){
                                      id
                                      name
                                      imageURL
                                      description
                                      price
                                }";


                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValue("id", itemId)
                    .Create();


                var result = await requestExecutor.ExecuteAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // GET: api/<CatalogController>
        [HttpGet]
        [Route("Item/GetItem/{categoryId}/{start}/{end}")]
        public async Task<IActionResult> GetItems(int categoryId, int start, int end)
        {
            if (categoryId <= 0)
            {
                return BadRequest("Invalid Category Id");
            }

            try
            {
                var query = @"query itemsWithPagination({id:$id,start:$start,end:$end}){
                                      id
                                      name
                                      description
                                      price
                                      cateogry{
                                         id
                                         name
                                         imageURL
                                      }
                                }";


                var variables = new Dictionary<string, object?>();

                variables.Add("id", categoryId);
                variables.Add("start", start);
                variables.Add("end", end);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();


                var result = await requestExecutor.ExecuteAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Item/AddItem")]
        public async Task<IActionResult> AddItem([FromBody] Item entity)
        {
            try
            {
                var query = @"mutation saveItem(($input: Item!)) {
                          saveItem(input: $input) {
                              name
                              description
                              price
                            }
                            error
                          }";

                var variables = new Dictionary<string, object?>();

                variables.Add("name", entity.Name);
                variables.Add("description", entity.Description);
                variables.Add("price", entity.Price);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();

                await requestExecutor.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Item/UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] Item entity)
        {
            try
            {
                var query = @"mutation updateItem(($input: Item!)) {
                          saveItem(input: $input) {
                              name
                              description
                              price
                            }
                            error
                          }";

                var variables = new Dictionary<string, object?>();

                variables.Add("name", entity.Name);
                variables.Add("description", entity.Description);
                variables.Add("price", entity.Price);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();

                await requestExecutor.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<CatalogController>/delete/1/2
        [HttpDelete("Item/DeleteItem/{catergoryId}/{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            if (itemId <= 0)
            {
                return BadRequest("Invalid Item Id");
            }
            try
            {
                var query = @"mutation deleteCategory(($input: int)) {
                          deleteItem(input: $input) {
                              id
                            }
                            error
                          }";

                var variables = new Dictionary<string, object?>();

                variables.Add("id", itemId);

                var request = QueryRequestBuilder.New()
                    .SetQuery(query)
                    .SetVariableValues(variables)
                    .Create();

                await requestExecutor.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
