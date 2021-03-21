using Application.Blog.Commands;
using Application.Blog.Queries;
using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrapCityBlogDemo.Controller
{
    [Authorize]
    public class BlogController : ApiController
    {
        private readonly ILogger<AccountController> _logger;

        public BlogController(ILogger<AccountController> logger)
        {
            _logger = logger;
           
        }
        [HttpGet("blogs")]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetAllBlogsQuery());
            return Ok(new ApiResponse(result, result.Count > 0 ?"Blogs" : "No record found."));
        }

        // GET: api/Quote/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            if (id > 0)
            {
                var result = await Mediator.Send(new GetBlogQuery { Id = id });
                if(result != null)
                return Ok(new ApiResponse(result, "Blog"));
                else
                    return BadRequest(new ApiResponse(new string[] { "Record not found." }, "Record not found."));
            }
            else
              return  BadRequest(new ApiResponse(new string[] { "Id is not valid."}, "Id is not valid."));
           
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateBlogCommand command)
        {
            var result = await Mediator.Send(command);
            if (result > 0)
            {
               
                return Ok(new ApiResponse(result, string.Format("Blog with Id {0} created.", result)));
            }
            else
                return BadRequest(new ApiResponse(new string[] { "Unable to create blog." }, "Unable to create blog."));

        }

      
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateBlogCommand command)
        {
            if (id == command.Id)
            {
                var result = await Mediator.Send(command); ;
                if (result > 0)
                {

                    return Ok(new ApiResponse(result, string.Format("Blog with Id {0} updated.", result)));
                }
                else
                    return BadRequest(new ApiResponse(new string[] { "Unable to update blog." }, "Unable to update blog."));
            }
            else
                return BadRequest(new ApiResponse(new string[] { "Id is not valid." }, "Id is not valid."));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteBlogCommand { Id = id });
            if (result > 0)
            {

                return Ok(new ApiResponse(result, string.Format("Blog with Id {0} deleted.", result)));
            }
            else
                return BadRequest(new ApiResponse(new string[] { "Unable to process request" }, "Unable to process request"));
        }
    }
}
