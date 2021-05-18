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
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await WorkinNotebad();
            Console.WriteLine("Done");
        }
        public static Task WorkinNotebad()
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
