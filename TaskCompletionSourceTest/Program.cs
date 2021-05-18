using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskCompletionSourceTest
{
    class Program
    {
        /// <summary>
        /// this programm opens notepad and listen when its closed for the continuation
        /// TaskCompletionSource schedule work on threadpool introduce a task from other types maynot have async and await
        /// it does not start async operation its just a away to create awaitable content which potentially gets a result
        /// who ever await wont proceed continuation unless gets the result
        /// use it to create awaitable code out of legacy code that does not leverage the task parralel library
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await WorkinNotebad();
            Console.WriteLine("Done");
        }
        /// <summary>
        /// public async static Task<object> WorkinNotebad()
        /// return await source.Task;
        /// if we introduced async and awiat to the following method is will inroduce another state machine that is not needed 
        /// we always should let the caller who the main method above to take care of the async operation 
        /// </summary>
        /// <returns></returns>
        
        public  static Task<object> WorkinNotebad()
        {
          var source = new TaskCompletionSource<object>();
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo("NotePad.exe")
                 {
                RedirectStandardError = true,UseShellExecute=false
                 }
            };

            process.Exited +=(sender,e)=>{
                source.SetResult(null);
                };
            process.Start();
            
              return source.Task;
        }
    }
}
