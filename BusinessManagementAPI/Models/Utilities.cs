namespace BusinessManagementAPI.Models
{
    public static class Utilities
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
