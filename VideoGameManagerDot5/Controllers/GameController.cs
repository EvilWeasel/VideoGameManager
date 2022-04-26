using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VideoGameManagerDot5.DataAccess;

namespace VideoGameManagerDot5.Controllers
{
    /// <summary>
    /// Create Controller 101:
    /// 1. Inherit from 'ControllerBase' inside 'Microsoft.AspNetCore.Mvc'
    /// 2. Don't forget, using angle-brackets, to declare the class a 'ApiController' and set the route
    ///     to a url to your liking (you can use 'Route("[controller]")' to set the route according to
    ///     the name of the controller)
    /// 3. If using EntityFramework: create private readonly context field
    /// 4. Generate constructor (If using EF: set your Context as a parameter and set the 
    ///     private context-field, this will come from the dependency-injection in Startup.cs)
    /// 5. Define a function, that will execute and return something and set the corresponding http-method
    ///     in angle-brackets above the function
    /// 6. Profit?
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly VideoGameDataContext context;
        public GameController(VideoGameDataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Game> GetAllGames()
        {
            return context.Games;
        }
    }
}
