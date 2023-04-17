namespace LabDarbas3_19.Class
{
    /// <summary>
    /// Interface for generic tables that can return a formatted header string.
    /// </summary>
    public interface ITableGeneric
    {
        /// <summary>
        /// Returns a header string formatted according to the specified format string.
        /// </summary>
        /// <param name="format">The format string to use for formatting the header.</param>
        /// <returns>A string representing the formatted header.</returns>
        string Header(string format);
    }
}
