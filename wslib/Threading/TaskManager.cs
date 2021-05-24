using System.Collections.Generic;
using System.Threading.Tasks;

namespace wslib.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskManager
    {
        /// <summary>
        /// 
        /// </summary>
        public SortedDictionary<string, Task> Tasks { get; set; }
    }
}