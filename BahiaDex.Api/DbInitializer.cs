namespace BahiaDex.Api
{
    /// <summary>
    /// Class to initialize the database.
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// Function to initialize the database.
        /// </summary>
        /// <param name="context">The db context.</param>
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}