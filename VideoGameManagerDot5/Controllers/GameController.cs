using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Here we add another route for getting a individual game from our db-context
        /// The "{id}" inside the route will be automatically populated from the url-parameters
        /// eg: http-get comes in with url: "localhost/Game/1"
        ///     --> Where the param right after "localhost/..." is the controller-route &
        ///         the parameter right after "Game/..." is the function-route => id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Game GetGameByID(int id) => context.Games.FirstOrDefault(g => g.ID == id);

        /// <summary>
        /// In this route, we use the http-delete-method and url-parameter of id, to find a
        /// specific Game from our database, save it to a variable, remove it from our context,
        /// sync the context with our database using the 'SaveChangesAsync()'-method and return
        /// the deleted game to the api-caller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<Game> DeleteGameByID(int id)
        {
            var gameToDelete = context.Games.Find(id);
            context.Games.Remove(gameToDelete);
            await context.SaveChangesAsync();
            return gameToDelete;
        }
    }
}
