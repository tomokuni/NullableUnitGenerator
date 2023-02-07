namespace System.CodeDom.Compiler
{
    /// <summary>
    /// CompilerError
    /// </summary>
    public class CompilerError
    {
        /// <summary>ErrorText</summary>
        public string? ErrorText { get; set; }

        /// <summary>IsWarning</summary>
        public bool IsWarning { get; set; }
    }

    /// <summary>
    /// CompilerErrorCollection
    /// </summary>
    public class CompilerErrorCollection
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="error"></param>
        public void Add(CompilerError error)
        {
        }
    }
}
