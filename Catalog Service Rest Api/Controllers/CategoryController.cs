using Catalog_Service_BLL;
using Catalog_Service_Rest_Api.HATEOAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Service_Rest_Api.Controllers
{
    [ApiController]
    [Route("[api/category]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IUrlHelper urlHelper;

        public CategoryController(ICategoryService categoryService, IUrlHelper injectedUrlHelper)
        {
            this.categoryService = categoryService;
            this.urlHelper = injectedUrlHelper;
        }

        [HttpGet(Name = nameof(Get))]
        [Authorize(Roles = "Manager, Buyer")]
        public IEnumerable<Category> Get()
        {
            return categoryService.GetCategories().Select(x => CreateLinksForCategory(x));
        }

        [HttpGet("{categoryid}/items/{page}", Name = nameof(GetItems))]
        [Authorize(Roles = "Manager, Buyer")]
        public IEnumerable<Item> GetItems([FromRoute] string categoryid, [FromRoute] int page)
        {
            return categoryService.GetItems(new Guid(categoryid), page).Select(x => CreateLinksForItem(x));
        }

        [HttpPost]
        [Consumes("application/json")]
        [Authorize(Roles = "Manager")]
        public IActionResult Post([FromBody] Category category)
        {
            if (category == null) return BadRequest();
            categoryService.AddCategory(category);
            return Ok();
        }

        [HttpPost("{categoryid}/items")]
        [Consumes("application/json")]
        [Authorize(Roles = "Manager")]
        public IActionResult PostItem([FromRoute] string categoryid, [FromBody] Item item)
        {
            if (string.IsNullOrEmpty(categoryid) || item == null) return BadRequest();
            categoryService.AddItem(new Guid(categoryid), item);

            return Ok();
        }

        [HttpPut]
        [Consumes("application/json")]
        [Authorize(Roles = "Manager")]
        public IActionResult Put([FromBody] Category category)
        {
            if (category == null) return BadRequest();

            categoryService.UpdateCategory(category);
            return Ok();
        }

        [HttpPut("{categoryid}/items")]
        [Consumes("application/json")]
        [Authorize(Roles = "Manager")]
        public IActionResult PutItem([FromBody] Item item)
        {
            if (item == null) return BadRequest();
            categoryService.UpdateItem(item);

            return Ok();
        }

        [HttpDelete("{categoryid}")]
        [Authorize(Roles = "Manager")]
        public IActionResult Delete([FromRoute] string categoryid)
        {
            if (string.IsNullOrEmpty(categoryid)) return BadRequest();

            categoryService.DeleteCategory(new Guid(categoryid));

            return Ok();
        }

        [HttpDelete("{categoryid}/items/{itemId}")]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteItem([FromRoute] string categoryid, [FromRoute] string itemId)
        {
            if (string.IsNullOrEmpty(categoryid) || string.IsNullOrEmpty(itemId)) return BadRequest();

            categoryService.DeleteItem(new Guid(itemId));

            return Ok();
        }

        private Category CreateLinksForCategory(Category category)
        {
            var idObj = new { id = category.Id };
            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.Get), idObj),
                "self",
                "GET"));

            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.Post), idObj),
                "post_category",
                "POST"));

            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.Put), idObj),
                "put_category",
                "PUT"));

            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.Delete), idObj),
                "delete_category",
                "DELETE"));

            return category;
        }

        private Item CreateLinksForItem(Item category)
        {
            var idObj = new { id = category.Id };
            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.GetItems), idObj),
                "self",
                "GET"));

            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.PostItem), idObj),
                "post_item",
                "POST"));

            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.PutItem), idObj),
                "put_item",
                "PUT"));

            category.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.DeleteItem), idObj),
                "delete_item",
                "DELETE"));

            return category;
        }

    }
}
