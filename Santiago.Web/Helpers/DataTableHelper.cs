namespace Santiago.Web.Helpers
{
    public static class DataTableHelper
    {
        public static int GetRightSkipNumber(int currentSkipNumber, int skipNumber, int itemsTotalCount)
        {
            if (currentSkipNumber != 0)
            {
                if (currentSkipNumber > itemsTotalCount)
                {
                    return (itemsTotalCount / skipNumber) * skipNumber;
                }
                
                if (currentSkipNumber == itemsTotalCount)
                {
                    return ((itemsTotalCount / skipNumber) - 1) * skipNumber;
                }
            }

            return currentSkipNumber;
        }
    }
}