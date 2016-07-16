namespace Santiago.Infrastructure.Repositories.DataProviding
{
    public static class ContextProvider
    {
        public static Context CreateNewContext()
        {
            return new Context();
        }
    }
}