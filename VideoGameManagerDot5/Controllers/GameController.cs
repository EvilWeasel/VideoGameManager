using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <summary>
        /// Grab the DataContext defined in Startup.cs using 'Constructor-Injection' inside the parameters,
        /// and assign the class-local context to the context we get through the dependency-injection
        /// </summary>
        /// <param name="context"></param>
        public GameController(VideoGameDataContext context)
        {
            this.context = context;
        }
        // [HttpGet]
        // public IEnumerable<Game> GetAllGames()
        // {
        //     return context.Games;
        // }

        // More concise and shorter version of the code above
        [HttpGet]
        public IEnumerable<Game> GetAllGames() => context.Games;

        /// <summary>
        /// In this method, we take in a new Game Object from the 'response.body' and use the
        /// 'context.Add'-Method to add it to the collection.
        /// We then asynchronously save the changes to the database and return the created game-object.
        /// When 'awaiting' something, we always need the 'async' keyword inside the function-signature
        /// and also return a Task of type 'Game' --> Task<Game>
        /// </summary>
        /// <param name="newGame"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Game> AddGame([FromBody]Game newGame)
        {
            context.Add(newGame);
            await context.SaveChangesAsync();
            return newGame;
        }
    }
}
