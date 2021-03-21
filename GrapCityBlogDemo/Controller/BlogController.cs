using Application.Blog.Commands;
using Application.Blog.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrapCityBlogDemo.Controller
{
    //[Authorize]
    public class BlogController : ApiController
    {
        
        [HttpGet("blogs")]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetAllBlogsQuery());
            return Ok(result);
        }

        // GET: api/Quote/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await Mediator.Send(new GetBlogQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateBlogCommand command)
        {
            var result =   await Mediator.Send(command);;
            return Ok();

        }

      
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateBlogCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBlogCommand { Id = id });

            return NoContent();
        }
    }
}
